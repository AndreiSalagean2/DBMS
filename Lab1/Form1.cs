using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace LibraryWinForms
{
    public partial class Form1 : Form
    {
        // ======= Câmpuri =======
        private readonly string _connString =
            ConfigurationManager.ConnectionStrings["LibraryDB"].ConnectionString;

        private SqlConnection _connection;
        private SqlDataAdapter _daAuthors, _daBooks;
        private DataSet _ds;
        private BindingSource _bsAuthors, _bsBooks;

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            button1.Click += BtnSave_Click;
        }

        private void dgvBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // ======= La pornirea formei =======
        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. Conexiune
            _connection = new SqlConnection(_connString);

            // 2. Adaptori
            _daAuthors = new SqlDataAdapter("SELECT * FROM Authors", _connection);
            new SqlCommandBuilder(_daAuthors);   // auto‑generează INSERT/UPDATE/DELETE

            _daBooks = new SqlDataAdapter("SELECT * FROM Books", _connection);
            new SqlCommandBuilder(_daBooks);

            // 3. DataSet & DataTables
            _ds = new DataSet();
            _daAuthors.Fill(_ds, "Authors");
            _daBooks.Fill(_ds, "Books");

            // 4. DataRelation 1:n
            _ds.Relations.Add(
                "FK_Authors_Books",
                _ds.Tables["Authors"].Columns["AuthorID"],
                _ds.Tables["Books"].Columns["AuthorID"]);

            // 5. BindingSource‑uri
            _bsAuthors = new BindingSource { DataSource = _ds, DataMember = "Authors" };
            _bsBooks = new BindingSource { DataSource = _bsAuthors, DataMember = "FK_Authors_Books" };

            // 6. Legare controale
            dgvAuthors.DataSource = _bsAuthors;
            dgvBooks.DataSource = _bsBooks;

            // 7. Aspect
            dgvAuthors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // ======= Butonul Save =======
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _bsBooks.EndEdit();
                _bsAuthors.EndEdit();

                _daBooks.Update(_ds.Tables["Books"]);
                _daAuthors.Update(_ds.Tables["Authors"]);

                MessageBox.Show("Modificările au fost salvate.",
                                "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la salvare: " + ex.Message,
                                "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}