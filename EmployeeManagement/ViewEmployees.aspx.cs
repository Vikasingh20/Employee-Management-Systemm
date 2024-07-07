using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManagement
{
    public partial class ViewEmployees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEmployee.aspx");
        }
        private void BindGrid()
        {
            string connectionString = "Data Source=IKL-68\\SQLEXPRESS;Initial Catalog=EmployeeManagement;User Id=sa;Password=sa@1234;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT EmployeeName, EmployeeCode, Salary, Year FROM Employees";

                
                if (!string.IsNullOrEmpty(drdYear.SelectedValue))
                {
                    if(drdYear.SelectedValue != "All")
                    {
                        query += $" WHERE Year = {drdYear.SelectedValue}";
                    }
                }

                if (!string.IsNullOrEmpty(txtEmpName.Text))
                {
                    query += string.IsNullOrEmpty(drdYear.SelectedValue) || drdYear.SelectedValue == "All" ? " WHERE" : " AND";
                    query += $" EmployeeName LIKE '%{txtEmpName.Text}%'";
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }


        protected void drdYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            drdYear.SelectedIndex = 0;
            txtEmpName.Text = "";
            BindGrid();
        }

        protected void txtEmpName_TextChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}