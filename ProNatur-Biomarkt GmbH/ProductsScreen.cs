using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProNatur_Biomarkt_GmbH
{
    public partial class ProductsScreen : Form
    {
        private SqlConnection databaseConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\Angelika\Documents\Pro-Natur Biomarkt GmbH.mdf;Integrated Security = True; Connect Timeout = 30");
        private int lastSelectProductKey;

        public ProductsScreen()
        {
            InitializeComponent();
            //Start
            ShowProducts();

        }

        private void ShowProducts()
        {

            databaseConnection.Open();
            string query = "select * from Products";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, databaseConnection);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            productDGV.DataSource = dataSet.Tables[0];

            productDGV.Columns[0].Visible = false;

            databaseConnection.Close();
        }


        private void btnProductSave_Click(object sender, EventArgs e)
        {
            if (textBoxProductName.Text == "" ||
                textBoxProductBrand.Text == "" ||
                textBoxProductPrice.Text == "" ||
                comboBoxProductCategory.Text == "")
            {
                MessageBox.Show("Bitte fülle alle Werte aus.");
                return;
            }
            //save product name in database
            string productName = textBoxProductName.Text;
            string productBrand = textBoxProductBrand.Text;
            string productCategory = comboBoxProductCategory.Text;
            string productPrice = textBoxProductPrice.Text;

            string query = string.Format("insert into Products values('{0}', '{1}', '{2}', '{3}')", productName, productBrand, productCategory, productPrice);
            ExecuteQuery(query);

            ClearAllFields();
            ShowProducts();

        }

        private void btnProductEdit_Click(object sender, EventArgs e)
        {
            if (lastSelectProductKey == 0)
            {
                MessageBox.Show("Bitte wähl zuerst ein Product aus.");
                return;
            }
            string productName = textBoxProductName.Text;
            string productBrand = textBoxProductBrand.Text;
            string productCategory = comboBoxProductCategory.Text;
            string productPrice = textBoxProductPrice.Text;

            string query = string.Format("update Products set Name ='{0}',Brand='{1}',Category='{2}', Price ='{3}' where Id={4}",
                productName, productBrand, productCategory, productPrice, lastSelectProductKey);
            ExecuteQuery(query);

            ShowProducts();
        }
        private void btnProductClear_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }
        private void btnProductDelete_Click(object sender, EventArgs e)
        {
            if (lastSelectProductKey == 0)
            {
                MessageBox.Show("Bitte wähl zuerst ein Product aus.");
                return;
            }
            string query = string.Format("delete from Products where Id= {0};", lastSelectProductKey);
            ExecuteQuery(query);
            ClearAllFields();
            ShowProducts();
        }

        private void ExecuteQuery(string query)
        {
            databaseConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, databaseConnection);
            sqlCommand.ExecuteNonQuery();
            databaseConnection.Close();
        }


        private void ClearAllFields()
        {
            textBoxProductName.Text = "";
            textBoxProductBrand.Text = "";
            textBoxProductPrice.Text = "";
            comboBoxProductCategory.Text = "";
            comboBoxProductCategory.SelectedItem = null;
        }

        private void productDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            textBoxProductName.Text = productDGV.SelectedRows[0].Cells[1].Value.ToString();
            textBoxProductBrand.Text = productDGV.SelectedRows[0].Cells[2].Value.ToString();
            comboBoxProductCategory.Text = productDGV.SelectedRows[0].Cells[3].Value.ToString();
            textBoxProductPrice.Text = productDGV.SelectedRows[0].Cells[4].Value.ToString();

            lastSelectProductKey = (int)productDGV.SelectedRows[0].Cells[0].Value;

        }

        private void btnRechnungerstellen_Click(object sender, EventArgs e)
        {
            RechnungScreen rechnungScreen = new RechnungScreen();
            rechnungScreen.Show();

            this.Hide();
        }
    }
}
