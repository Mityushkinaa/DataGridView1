using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Npgsql;
using System.Data;

namespace DataGridViewIUD
{
    public partial class FormDgvIud : Form
    {
        private readonly string _sConnStr = new NpgsqlConnectionStringBuilder
        {
            Host = Database.Default.Host,
            Port = Database.Default.Port,
            Database = "univers",
            Username = "postgres",
            Password = "1234",
            MaxAutoPrepare = 10,
            AutoPrepareMinUsages = 2
        }.ConnectionString;

        public FormDgvIud()
        {
            InitializeComponent();
            InitializeDgvMyOwn();
            InitializeDgvMyDist();
        }

        private void InitializeDgvMyDist()
        {
            dgvMyDist.Rows.Clear();
            dgvMyDist.Columns.Clear();
            dgvMyDist.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "id",
                Visible = false
            });

            dgvMyDist.Columns.Add("name", "Название района");
            dgvMyDist.Columns.Add("area", "Площадь района");

            var districtCountry = new DataGridViewComboBoxColumn
            {
                Name = "countryName",
                HeaderText = @"Страна",
                DisplayMember = "name",
                ValueMember = "id"
            };


            dgvMyDist.Columns.AddRange(districtCountry);
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();

                using (var sCommand = new NpgsqlCommand())
                {
                    sCommand.CommandText = "SELECT * FROM country";
                    sCommand.Connection = sConn;
                    var table = new DataTable();
                    table.Load(sCommand.ExecuteReader());
                    districtCountry.DataSource = table;
                }
                using (var sCommand = new NpgsqlCommand())
                {
                    sCommand.CommandText = "SELECT *, country.id as countryId, country.name as countryName  FROM district JOIN country ON district.country_id = country.id";
                    sCommand.Connection = sConn;
                    var reader = sCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        var dataDict = new Dictionary<string, object>();
                        foreach (var columnsName in new[] { "name", "area", "countryName" })
                        {
                            dataDict[columnsName] = reader[columnsName];
                        }
                        var rowIdx = dgvMyDist.Rows.Add(reader["id"], reader["name"], reader["area"],
                            reader["countryId"]);
                        dgvMyDist.Rows[rowIdx].Tag = dataDict;
                    }
                }
            }
        }

        private void InitializeDgvMyOwn()
        {
            dgvMyOwn.Rows.Clear();
            dgvMyOwn.Columns.Clear();
            dgvMyOwn.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "id",
                Visible = false
            });
            dgvMyOwn.Columns.Add("name", "Названия страны");
            dgvMyOwn.Columns.Add("language", "Язык");


            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = @"SELECT id, name, language
                                    FROM country
                                    ORDER BY id"
                };
                var reader = sCommand.ExecuteReader();
                while (reader.Read())
                {
                    var dataDict = new Dictionary<string, object>();
                    foreach (var columnsName in new[] { "name", "language" })
                    {

                        dataDict[columnsName] = reader[columnsName];
                    }

                    var rowIdx = dgvMyOwn.Rows.Add(reader["id"], reader["name"], reader["language"]);

                    dgvMyOwn.Rows[rowIdx].Tag = dataDict;
                }
            }
        }

        private void dgvMu_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {


            var row = dgvMyOwn.Rows[e.RowIndex];
            bool newRow = row.IsNewRow;
            int rep = 0;
            if (dgvMyOwn.IsCurrentRowDirty)
            {

                foreach (DataGridViewRow rw in dgvMyOwn.Rows)
                {
                    var dataDict = new Dictionary<string, object>();
                    foreach (var columnsName in new[] { "name", "language" })
                    {
                        dataDict[columnsName] = row.Cells[columnsName].Value;
                    }

                    if (row == rw || rw.IsNewRow)
                    {
                        continue;
                    }

                    var dataDict2 = ((Dictionary<string, object>)rw.Tag);
                    if ((Convert.ToString(dataDict2["name"]).Trim() == Convert.ToString(dataDict["name"]).Trim()) && (Convert.ToString(dataDict2["language"]) == Convert.ToString(dataDict["language"])))
                    {
                        row.ErrorText = $"Значение в строке уже существует";
                        e.Cancel = true;
                        rep = 0;

                    }
                }

                if (rep > 1)
                {

                }
                var cellsWithPotentialErrors = new[] { row.Cells["name"], row.Cells["language"] };
                foreach (var cell in cellsWithPotentialErrors)
                {
                    if (string.IsNullOrWhiteSpace(Convert.ToString(cell.Value)))
                    {
                        row.ErrorText = $"Значение в столбце '{cell.OwningColumn.HeaderText}' не может быть пустым";
                        e.Cancel = true;
                    }
                }




                if (!e.Cancel)
                {
                    using (var sConn = new NpgsqlConnection(_sConnStr))
                    {
                        sConn.Open();
                        var sCommand = new NpgsqlCommand
                        {
                            Connection = sConn
                        };
                        sCommand.Parameters.AddWithValue("@NAME", row.Cells["name"].Value);
                        sCommand.Parameters.AddWithValue("@LANG", row.Cells["language"].Value);

                        var ownId = (int?)row.Cells["ID"].Value;
                        if (ownId.HasValue)
                        {
                            sCommand.CommandText = @"UPDATE country SET name = @NAME, language = @LANG
                                                    WHERE id = @ownID";
                            sCommand.Parameters.AddWithValue("@ownID", ownId.Value);
                            sCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            sCommand.CommandText = @"INSERT INTO country(name, language)
                                                     VALUES (@NAME, @LANG)
                                                     RETURNING id";
                            row.Cells["id"].Value = sCommand.ExecuteScalar();
                        }

                        var dataDict = new Dictionary<string, object>();
                        foreach (var columnsName in new[] { "name", "language" })
                        {
                            dataDict[columnsName] = row.Cells[columnsName].Value;
                        }

                        row.Tag = dataDict;
                    }

                    row.ErrorText = "";
                    foreach (var cell in cellsWithPotentialErrors)
                    {
                        cell.ErrorText = "";
                    }


                }
            }
        }

        private void dgvMu_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!dgvMyOwn.Rows[e.RowIndex].IsNewRow)
            {
                dgvMyOwn[e.ColumnIndex, e.RowIndex].ErrorText = "Значение изменено и пока не сохранено.";
                if (dgvMyOwn.Rows[e.RowIndex].Tag != null)
                    dgvMyOwn[e.ColumnIndex, e.RowIndex].ErrorText += "\nПредыдущее значение - " +
                                                                  ((Dictionary<string, object>)dgvMyOwn.Rows[e.RowIndex]
                                                                      .Tag)[dgvMyOwn.Columns[e.ColumnIndex].Name];
            }
        }

        private void dgvMu_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var ownId = (int?)e.Row.Cells["id"].Value; // проверка на существование Id
            if (ownId.HasValue)
            {
                using (var sConn = new NpgsqlConnection(_sConnStr))
                {
                    sConn.Open();
                    var sCommand = new NpgsqlCommand
                    {
                        Connection = sConn,
                        CommandText = "DELETE  FROM country WHERE id = @ownID"
                    };
                    sCommand.Parameters.AddWithValue("@ownID", ownId.Value);
                    sCommand.ExecuteNonQuery();
                }
            }
        }

        private void dgvMu_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && dgvMyOwn.IsCurrentRowDirty)
            {
                dgvMyOwn.CancelEdit();
                if (dgvMyOwn.CurrentRow.Cells["id"].Value != null)
                {
                    foreach (var kvp in (Dictionary<string, object>)dgvMyOwn.CurrentRow.Tag)
                    {
                        dgvMyOwn.CurrentRow.Cells[kvp.Key].Value = kvp.Value;
                        dgvMyOwn.CurrentRow.Cells[kvp.Key].ErrorText = "";
                    }
                }
                else
                {
                    dgvMyOwn.Rows.Remove(dgvMyOwn.CurrentRow);
                }
            }
        }

        private void dgvMyDist_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            var row = dgvMyDist.Rows[e.RowIndex];
            if (dgvMyDist.IsCurrentRowDirty)
            {
                foreach (DataGridViewRow rw in dgvMyDist.Rows)
                {
                    var dataDict = new Dictionary<string, object>();
                    foreach (var columnsName in new[] { "name", "area" })
                    {
                        dataDict[columnsName] = row.Cells[columnsName].Value;
                    }
                    dataDict["countryName"] = row.Cells["countryName"].FormattedValue;

                    if (row == rw || rw.IsNewRow)
                    {
                        continue;
                    }

                    var dataDict2 = ((Dictionary<string, object>)rw.Tag);
                    if ((Convert.ToString(dataDict2["name"]).Trim() == Convert.ToString(dataDict["name"]).Trim()) && (Convert.ToInt32(dataDict2["area"]) == Convert.ToInt32(dataDict["area"])) && (Convert.ToString(dataDict2["countryName"]) == Convert.ToString(dataDict["countryName"])))
                    {
                        row.ErrorText = $"Значение в строке уже существует";
                        e.Cancel = true;

                    }
                }

                var cellsWithPotentialErrors = new[] { row.Cells["countryName"] };
                foreach (var cell in cellsWithPotentialErrors)
                {
                    if (string.IsNullOrWhiteSpace(Convert.ToString(cell.Value)))
                    {
                        row.ErrorText = $"Значение в столбце '{cell.OwningColumn.HeaderText}' не может быть пустым";
                        e.Cancel = true;
                    }
                }
                if (Convert.ToString(row.Cells["name"].Value).Length > 100)
                {
                    row.ErrorText = $"Значение в столбце '{row.Cells["name"].OwningColumn.HeaderText}' не может быть такой длинны";
                    e.Cancel = true;
                }

                int z;
                if (!Int32.TryParse(Convert.ToString(row.Cells["area"].Value), out z))
                {
                    row.ErrorText = $"Значение в столбце '{row.Cells["area"].OwningColumn.HeaderText}' не может быть текстовым";
                    e.Cancel = true;
                }
                else
                {
                    if ((Convert.ToInt32(row.Cells["area"].Value) <= 0))
                    {
                        row.ErrorText = $"Значение в столбце '{row.Cells["area"].OwningColumn.HeaderText}' не может быть отрицательным";
                        e.Cancel = true;
                    }
                }


                if (!e.Cancel)
                {
                    using (var sConn = new NpgsqlConnection(_sConnStr))
                    {
                        sConn.Open();
                        var sCommand = new NpgsqlCommand
                        {
                            Connection = sConn
                        };
                        sCommand.Parameters.AddWithValue("@Name", row.Cells["name"].Value);
                        sCommand.Parameters.AddWithValue("@area", Convert.ToInt32(row.Cells["area"].Value));
                        sCommand.Parameters.AddWithValue("@Country", row.Cells["countryName"].Value);
                        var ownId = (int?)row.Cells["ID"].Value;
                        if (ownId.HasValue)
                        {
                            sCommand.CommandText = @"UPDATE district SET name = @Name, area = @Area, 
                                                            country_id = @Country
                                                    WHERE id = @ownID";
                            sCommand.Parameters.AddWithValue("@ownID", ownId.Value);
                            sCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            sCommand.CommandText = @"INSERT INTO district(name, area, country_id)
                                                     VALUES (@Name, @Area, @Country)
                                                     RETURNING id";
                            row.Cells["id"].Value = sCommand.ExecuteScalar();
                        }

                        var dataDict = new Dictionary<string, object>();
                        foreach (var columnsName in new[] { "name", "area" })
                        {
                            dataDict[columnsName] = row.Cells[columnsName].Value;
                        }
                        dataDict["countryName"] = row.Cells["countryName"].FormattedValue;

                        row.Tag = dataDict;
                    }

                    row.ErrorText = "";
                    foreach (var cell in cellsWithPotentialErrors)
                    {
                        cell.ErrorText = "";
                    }

                    // row.Cells["modified_date"].ErrorText = "";
                }
            }
        }

        private void dgvMyDist_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {

            var ownId = (int?)e.Row.Cells["id"].Value; // проверка на существование Id
            if (!ownId.HasValue) return;
            using (var sConn = new NpgsqlConnection(_sConnStr))
            {
                sConn.Open();
                var sCommand = new NpgsqlCommand
                {
                    Connection = sConn,
                    CommandText = "DELETE FROM district WHERE country_id = @ownID"
                };
                sCommand.Parameters.AddWithValue("@ownID", ownId.Value);
                sCommand.ExecuteNonQuery();
             
                sConn.Close();
                InitializeDgvMyDist();
            }
        }
    

        private void dgvMyDist_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && dgvMyDist.IsCurrentRowDirty)
            {
                dgvMyDist.CancelEdit();
                if (dgvMyDist.CurrentRow.Cells["id"].Value != null)
                {
                    foreach (var kvp in (Dictionary<string, object>)dgvMyDist.CurrentRow.Tag)
                    {
                        dgvMyDist.CurrentRow.Cells[kvp.Key].Value = kvp.Value;
                        dgvMyDist.CurrentRow.Cells[kvp.Key].ErrorText = "";
                    }
                }
                else
                {
                    dgvMyDist.Rows.Remove(dgvMyDist.CurrentRow);
                }
            }
        }

        private void dgvMyDist_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!dgvMyDist.Rows[e.RowIndex].IsNewRow)
            {
                dgvMyDist[e.ColumnIndex, e.RowIndex].ErrorText = "Значение изменено и пока не сохранено.";
                if (dgvMyDist.Rows[e.RowIndex].Tag != null)
                    dgvMyDist[e.ColumnIndex, e.RowIndex].ErrorText += "\nПредыдущее значение - " +
                                                                  ((Dictionary<string, object>)dgvMyDist.Rows[e.RowIndex]
                                                                      .Tag)[dgvMyDist.Columns[e.ColumnIndex].Name];
            }

        }
     

        private void dgvMyOwn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}