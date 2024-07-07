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
    public partial class AddEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=IKL-68\\SQLEXPRESS;Initial Catalog=EmployeeManagement;User Id=sa;Password=sa@1234;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Employees (EmployeeName, EmployeeCode, Salary, Year) VALUES (@EmployeeName, @EmployeeCode, @Salary, @Year)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                 if(txtEmpName.Text != "" && txtEmpCode.Text != "" && txtSalary.Text != "" && drdYear.SelectedIndex != 0)
                 {
                        command.Parameters.AddWithValue("@EmployeeName", txtEmpName.Text);
                        command.Parameters.AddWithValue("@EmployeeCode", txtEmpCode.Text);
                        command.Parameters.AddWithValue("@Salary", Convert.ToDecimal(txtSalary.Text));
                        command.Parameters.AddWithValue("@Year", Convert.ToInt32(drdYear.SelectedValue));

                        connection.Open();
                        int row = command.ExecuteNonQuery();
                        if (row > 0)
                        {
                            Response.Redirect("ViewEmployees.aspx");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Invalid Input!')", true);
                    } 
                    
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtEmpName.Text = "";
            txtSalary.Text = "";
            txtEmpCode.Text = "";
            drdYear.SelectedIndex = 0;
        }
    }
}