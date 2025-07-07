using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryWinForms
{
    public partial class Form1 : Form
    {
        // === configurare ===
        private readonly NameValueCollection cfg =
            (NameValueCollection)ConfigurationManager.GetSection("masterDetail");
        private readonly string connStr =
            ConfigurationManager.ConnectionStrings["SqlCn"].ConnectionString;

        // === ADO.NET ===
        private SqlConnection cn;
        private SqlDataAdapter daParent, daChild;
        private DataSet ds;
        private BindingSource bsParent, bsChild;

        // === nume scenariu curent ===
        private string scenario;
        private ScenarioKeys keys; 

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            button1.Click += BtnSave_Click;   // button1 = Save
        }

        /* ------------------ FORM LOAD ------------------ */
        private void Form1_Load(object sender, EventArgs e)
        {
            scenario = cfg["ActiveScenario"] ?? string.Empty;
            if (string.IsNullOrEmpty(scenario))
            {
                MessageBox.Show("ActiveScenario missing in App.config.");
                Close();
                return;
            }

            keys = new ScenarioKeys(cfg, scenario);
            Text = keys.Caption ?? "Master‑Detail";

            // Conexiune
            cn = new SqlConnection(connStr);

            // Adaptori
            daParent = new SqlDataAdapter(keys.ParentSelect, cn);
            daChild = new SqlDataAdapter(keys.ChildSelect, cn);

            // DataSet
            ds = new DataSet();
            daParent.Fill(ds, "Parent");
            daChild.Fill(ds, "Child");

            // Relaţie 1:n
            ds.Relations.Add("FK",
                ds.Tables["Parent"].Columns[keys.ParentKey],
                ds.Tables["Child"].Columns[keys.ChildForeignKey]);

            // BindingSource‑uri
            bsParent = new BindingSource { DataSource = ds, DataMember = "Parent" };
            bsChild = new BindingSource { DataSource = bsParent, DataMember = "FK" };
            dgvAuthors.DataSource = bsParent;
            dgvBooks.DataSource = bsChild;

            
            BuildChildCrudCommands();
        }

        /* --------------- BUILD CRUD COMMANDS --------------- */
        private void BuildChildCrudCommands()
        {
            // Dacă lipsesc numele procedurilor, lăsăm SqlCommandBuilder
            if (string.IsNullOrWhiteSpace(keys.ChildInsertProc) &&
                string.IsNullOrWhiteSpace(keys.ChildUpdateProc) &&
                string.IsNullOrWhiteSpace(keys.ChildDeleteProc))
            {
                new SqlCommandBuilder(daChild);
                return;
            }

            // INSERT
            if (!string.IsNullOrWhiteSpace(keys.ChildInsertProc))
            {
                var cmdIns = new SqlCommand(keys.ChildInsertProc, cn)
                { CommandType = CommandType.StoredProcedure };
                AddParamsFromTable(cmdIns, ds.Tables["Child"], includePK: false);
                daChild.InsertCommand = cmdIns;
            }
            // UPDATE
            if (!string.IsNullOrWhiteSpace(keys.ChildUpdateProc))
            {
                var cmdUpd = new SqlCommand(keys.ChildUpdateProc, cn)
                { CommandType = CommandType.StoredProcedure };
                AddParamsFromTable(cmdUpd, ds.Tables["Child"], includePK: true);
                daChild.UpdateCommand = cmdUpd;
            }
            // DELETE
            if (!string.IsNullOrWhiteSpace(keys.ChildDeleteProc))
            {
                var cmdDel = new SqlCommand(keys.ChildDeleteProc, cn)
                { CommandType = CommandType.StoredProcedure };
                cmdDel.Parameters.Add("@" + keys.ChildPrimaryKey, SqlDbType.Int, 0, keys.ChildPrimaryKey);
                daChild.DeleteCommand = cmdDel;
            }
        }

        // adaugă parametri în funcţie de coloanele tabelului
        private void AddParamsFromTable(SqlCommand cmd, DataTable tbl, bool includePK)
        {
            foreach (DataColumn col in tbl.Columns)
            {
                if (!includePK && col.ColumnName.Equals(keys.ChildPrimaryKey, StringComparison.OrdinalIgnoreCase))
                    continue;

                SqlDbType type = MapType(col.DataType);
                cmd.Parameters.Add("@" + col.ColumnName, type, col.MaxLength < 0 ? 0 : col.MaxLength,
                    col.ColumnName);
            }
            if (includePK && !cmd.Parameters.Contains("@" + keys.ChildPrimaryKey))
            {
                cmd.Parameters.Add("@" + keys.ChildPrimaryKey, SqlDbType.Int, 0, keys.ChildPrimaryKey);
            }
        }

        private SqlDbType MapType(Type t) => t == typeof(int) ? SqlDbType.Int :
                                              t == typeof(string) ? SqlDbType.NVarChar :
                                              t == typeof(DateTime) ? SqlDbType.DateTime : SqlDbType.Variant;

        /* ------------------ SAVE ------------------ */
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Validate();
                bsChild.EndEdit();
                bsParent.EndEdit();
                daChild.Update(ds.Tables["Child"]);
                daParent.Update(ds.Tables["Parent"]);
                MessageBox.Show("Changes saved!", Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

       


        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        
        private struct ScenarioKeys
        {
            public string Caption;
            public string ParentSelect;
            public string ChildSelect;
            public string ParentKey;
            public string ChildForeignKey;
            public string ChildInsertProc;
            public string ChildUpdateProc;
            public string ChildDeleteProc;
            public string ChildPrimaryKey;

            public ScenarioKeys(NameValueCollection cfg, string scenario)
            {
                string p = scenario + ".";
                Caption = cfg[p + "Caption"];
                ParentSelect = cfg[p + "ParentSelect"];
                ChildSelect = cfg[p + "ChildSelect"];
                ParentKey = cfg[p + "ParentKey"];
                ChildForeignKey = cfg[p + "ChildForeignKey"];
                ChildInsertProc = cfg[p + "ChildInsert"];
                ChildUpdateProc = cfg[p + "ChildUpdate"];
                ChildDeleteProc = cfg[p + "ChildDelete"];

                // presupunem PK detail = prima col. din SELECT dacă nu e specificat
                ChildPrimaryKey = "BookID"; // pentru demo;
            }
        }
    }
}
