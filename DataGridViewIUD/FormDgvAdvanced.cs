using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace DataGridViewIUD
{
    public partial class FormDgvAdvanced : Form
    {
        private readonly string _sConnStr = new NpgsqlConnectionStringBuilder
        {
            Host = Database.Default.Host,
            Port = Database.Default.Port,
            Database = Database.Default.Name,
            Username = "postgres",
            Password = "1234",
            MaxAutoPrepare = 10,
            AutoPrepareMinUsages = 2
        }.ConnectionString;

        public FormDgvAdvanced()
        {
            InitializeComponent();
            InitializeDgvResources();
            InitializeDgvMu();
        }

        private void InitializeDgvResources()
        {
            dgvResources.Rows.Clear();
            dgvResources.Columns.Clear();
            var muNameColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = @"Название МО",
                DisplayMember = "name",
                ValueMember = "id"
            };
            var animalsNameColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = @"Название животного",
                DisplayMember = "name",
                ValueMember = "id"
            };
            var resourcesNumber = new DataGridViewTextBoxColumn
            {
                HeaderText = @"Численность"
            };
            var isNumberOddColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = @"Нечётность численности"
            };
            dgvResources.Columns.AddRange(muNameColumn, animalsNameColumn, resourcesNumber, isNumberOddColumn);
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                using (var sCommand = new NpgsqlCommand())
                {
                    sCommand.CommandText = "SELECT * FROM municipal_units";
                    sCommand.Connection = sConn;
                    var table = new DataTable();
                    table.Load(sCommand.ExecuteReader());
                    muNameColumn.DataSource = table;
                }
                using (var sCommand = new NpgsqlCommand())
                {
                    sCommand.CommandText = "SELECT * FROM animals";
                    sCommand.Connection = sConn;
                    var table = new DataTable();
                    table.Load(sCommand.ExecuteReader());
                    animalsNameColumn.DataSource = table;
                }
                using (var sCommand = new NpgsqlCommand())
                {
                    sCommand.CommandText = "SELECT *, number % 2 as is_odd FROM resources";
                    sCommand.Connection = sConn;
                    var reader = sCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        dgvResources.Rows.Add(reader["municipal_unit_id"], reader["animal_id"], reader["number"],
                            reader["is_odd"]);
                    }
                }
            }
        }

        private void dgvResources_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvResources.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
            {
                var comboboxCell = dgvResources[e.ColumnIndex, e.RowIndex] as DataGridViewComboBoxCell;
                MessageBox.Show($"Value: {comboboxCell.Value}, Formatted value: {comboboxCell.FormattedValue}");
            }
            else if (dgvResources.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
            {
                var checkboxCell = dgvResources[e.ColumnIndex, e.RowIndex] as DataGridViewCheckBoxCell;
                MessageBox.Show($"Checkbox value: {checkboxCell.Value}");
            }
        }

        private void InitializeDgvMu()
        {
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = "SELECT * FROM municipal_units"
                };
                var dataAdapter = new NpgsqlDataAdapter(sCommand);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                dgvMu.ReadOnly = true;
                dgvMu.DataSource = ds.Tables[0];
            }
        }

        private void FormDgvAdvanced_Load(object sender, EventArgs e)
        {

        }
    }
}
