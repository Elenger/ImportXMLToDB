using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ImportXMLToDB
{
    public partial class ImportFromXML : Form
    {
        private string _connectionString;
        private string _xmlPath;

        public ImportFromXML()
        {
            InitializeComponent();
        }

        private void ButtonImport_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(_connectionString);     
            try
            {
                connection.Open();
            }
            catch
            {
                MessageBox.Show("Path to databese is not specified. Please select .mdf file");
                return;
            }

            XDocument document = new XDocument();
            try
            {
                document = XDocument.Load(_xmlPath);             
            }
            catch
            {
                MessageBox.Show("Path to XML is not specified. Please select XML file");
                return;
            }
            
            List<XNode> nodesList = document.DescendantNodes().ToList();

            foreach (XNode node in nodesList)
            {
                if (node.Parent == null) continue;

                Dictionary<string, string> dictionaryAttribute = new Dictionary<string, string>();
                XElement element = node as XElement;

                foreach (XAttribute attribute in element.Attributes())
                {
                    dictionaryAttribute.Add(attribute.Name.ToString(), attribute.Value);
                }

                Hashtable hashtableAttribute = GetConvertedHashtable(dictionaryAttribute);

                string queryString = "insert into Clients (CARDCODE,STARTDATE,FINISHDATE,LASTNAME,FIRSTNAME,SURNAME,GENDER, "
                    + "BIRTHDAY,PHONEHOME,PHONEMOBIL,EMAIL,CITY,STREET,HOUSE,APARTMENT) " +
                    "Values (@CARDCODE,@STARTDATE,@FINISHDATE,@LASTNAME,@FIRSTNAME,@SURNAME,@GENDER, "
                    + "@BIRTHDAY,@PHONEHOME,@PHONEMOBIL,@EMAIL,@CITY,@STREET,@HOUSE,@APARTMENT) ";

                SqlCommand command = new SqlCommand(queryString, connection);

                foreach(string attributeName in hashtableAttribute.Keys)
                {
                    command.Parameters.Add(SqlHelper.GetSqlParameter(attributeName));
                    command.Parameters["@" + attributeName].Value = hashtableAttribute[attributeName];
                }

                try
                {
                    command.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("The client with the current value of CARDCODE has already been added to the database. Unable to add duplicate CARDCODE unique identifier");
                    return;
                }
            }
            MessageBox.Show("Data is written to the database!");
            connection.Close();

            ReadClientsTable();
        }
        private Hashtable GetConvertedHashtable(Dictionary<string, string> dictionaryAttribute)
        {
            Hashtable hashtableAttribute = new Hashtable();

            foreach (KeyValuePair<string, string> keyValuePair in dictionaryAttribute)
            {
                if (keyValuePair.Value.Length == 0)
                {
                    hashtableAttribute[keyValuePair.Key] = DBNull.Value;
                    continue;
                }

                if (keyValuePair.Key == "CARDCODE")
                {
                    hashtableAttribute.Add(keyValuePair.Key, Decimal.Parse(keyValuePair.Value));
                }
                else if (keyValuePair.Key == "STARTDATE" || keyValuePair.Key == "FINISHDATE" || keyValuePair.Key == "BIRTHDAY")
                {
                    try
                    {
                        hashtableAttribute.Add(keyValuePair.Key, DateTime.Parse(keyValuePair.Value));
                    }
                    catch
                    {
                        MessageBox.Show("Some of the data was incorrectly formatted and written as null");
                        hashtableAttribute.Add(keyValuePair.Key, DBNull.Value);
                    }
                }
                else if (keyValuePair.Key == "APARTMENT")
                {
                    try
                    {
                        hashtableAttribute.Add(keyValuePair.Key, Int32.Parse(keyValuePair.Value));
                    }
                    catch
                    {
                        MessageBox.Show("Some of the data was incorrectly formatted and written as null");
                        hashtableAttribute.Add(keyValuePair.Key, DBNull.Value);
                    }
                }
                else
                {
                    hashtableAttribute.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }

            return hashtableAttribute;
        }
        private void ButtonRead_Click(object sender, EventArgs e)
        {
            ReadClientsTable();
        }
        private void ReadClientsTable()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
            }
            catch
            {
                MessageBox.Show("Path to databese is not specified. Please select .mdf file");
                return;
            }

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Clients", connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGrid.DataSource = dataTable;

            connection.Close();
        }

        private void ButtonSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _xmlPath = openFileDialog.FileName;
            }
        }

        private void ButtonSelectDB_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = " +
                   openFileDialog.FileName +
                    "; Integrated Security = True; Connect Timeout = 30";
            }
        }

        #region DataGrid 
        //check if the cells are filled correctly
        private void DataGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell currentCell = dataGrid[e.ColumnIndex, e.RowIndex];
            if (currentCell.EditedFormattedValue.ToString() == "")
            {
                dataGrid.CancelEdit();
                return;
            }
            object convertedValue = null;

            if (e.ColumnIndex == 0 && 
                currentCell.FormattedValue.ToString() != currentCell.EditedFormattedValue.ToString()) //cardcode
            {
                MessageBox.Show("Impossible to change the unique identifier");
                dataGrid.CancelEdit();
                return;
            }

            try
            {
                convertedValue = Convert.ChangeType(currentCell.EditedFormattedValue, currentCell.ValueType);
            }
            catch
            {
                MessageBox.Show("Wrong data format. Please use the format — " + currentCell.ValueType.Name);
                dataGrid.CancelEdit();
            }
        }

        //writing changes to the database
        private void DataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            object cardcode = dataGrid[0, e.RowIndex].Value;
            string parametrName = dataGrid.Columns[e.ColumnIndex].Name;
            object newValue = dataGrid[e.ColumnIndex, e.RowIndex].Value;

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string queryString = @"Update Clients
                                Set " + parametrName + " = @" + parametrName + " " +
                                "Where CARDCODE = @CARDCODE";

            SqlCommand command = new SqlCommand(queryString, connection);
            if (parametrName != "CARDCODE")
            {
                command.Parameters.Add(new SqlParameter("@CARDCODE", SqlDbType.Decimal, 20));
                command.Parameters["@CARDCODE"].Value = cardcode;
            }

            if (newValue.ToString() != "")
            {
                command.Parameters.Add(SqlHelper.GetSqlParameter(parametrName));
            }
            else command.Parameters.Add(new SqlParameter("@" + parametrName, DBNull.Value));

            command.Parameters["@" + parametrName].Value = newValue;

            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();

            ReadClientsTable();
        }

        //lock the last row of the table = prevent adding new clients from the grid
        private void DataGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            object cardcode = dataGrid[0, e.RowIndex].Value;
            if (cardcode == DBNull.Value || cardcode == null)
            {
                dataGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            else
            {
                dataGrid.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            }
        }
        #endregion  
    }
}
