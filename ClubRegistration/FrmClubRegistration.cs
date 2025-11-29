using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClubRegistration
{
    public partial class FrmClubRegistration : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;

        private int ID;
        private int Age;
        private int count;

        private string FirstName;
        private string MiddleName;
        private string LastName;
        private string Gender;
        private string Program;

        private long StudentId;

        public FrmClubRegistration()
        {
            InitializeComponent();
            clubRegistrationQuery = new ClubRegistrationQuery();
        }

        private void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();

            dataGridView1.DataSource = clubRegistrationQuery.bindingSource;
        }

        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }

        private int RegistrationID()
        {
            count++;
            return count;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            ID = RegistrationID();

            if (long.TryParse(txtStudentId.Text, out long studentIdValue))
            {
                StudentId = studentIdValue;
            }
            else
            {
                MessageBox.Show("Invalid Student ID format. Please enter a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FirstName = txtFirstName.Text;
            MiddleName = txtMiddleName.Text;
            LastName = txtLastName.Text;

            if (int.TryParse(txtAge.Text, out int ageValue))
            {
                Age = ageValue;
            }
            else
            {
                MessageBox.Show("Invalid Age format. Please enter a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            Gender = cbGender.Text;
            Program = cbProgram.Text;

            try
            {
                clubRegistrationQuery.RegisterStudent(ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program);

                MessageBox.Show("Student registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefreshListOfClubMembers();

                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Error: {ex.Message}", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputFields()
        {
            txtStudentId.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            cbGender.SelectedIndex = -1;
            cbProgram.SelectedIndex = -1; 
        }
    }
}
