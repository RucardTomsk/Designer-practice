using Alpha.Interfaces;
using Alpha.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alpha
{
    public partial class PopupWindowForAddCheckpoint : Form
    {
        private IDetailing detail;
        private DataStorageService dataStorageService = DataStorageService.GetInstance();
        public PopupWindowForAddCheckpoint(IDetailing detail)
        {
            this.detail = detail;
            this.Text = $"Add checkpoint for {detail.GetName()}";
            InitializeComponent();
        }

        public PopupWindowForAddCheckpoint(IDetailing detail, Checkpoint _checkpoint)
        {
            this.detail = detail;
            this.Text = $"Add checkpoint for Influence of the manager";
            Checkpoint checkpoint = new Checkpoint(_checkpoint.GetName() + " Influence of the manager", "Influence of the manager on " + _checkpoint.GetName(), 0, detail, _checkpoint.GetSpecialId()+"C");
            detail.AddCheckpoint(checkpoint);
            dataStorageService.AddCheckpoint(checkpoint);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string stateName = checkpointNameInput.Text;
            if (stateName == null || stateName == "")
            {
                MessageBox.Show("Please enter checkpoint's name", "Nullable name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string stateDescription = checkpointDescriptionInput.Text;
            if (stateDescription == null || stateDescription == "")
            {
                MessageBox.Show("Please enter checkpoint's description", "Nullable description", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int stateOrder = detail.GetCheckpoints().Count() * 10;
            string specialId = (specialIdInput.Text == "") ? null : specialIdInput.Text;
            Checkpoint checkpoint = new Checkpoint(stateName, stateDescription, stateOrder, detail, specialId);
            detail.AddCheckpoint(checkpoint);
            dataStorageService.AddCheckpoint(checkpoint);
            this.Close();
        }
    }
}
