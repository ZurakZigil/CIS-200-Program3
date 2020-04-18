// Program 3
// CIS 200-01
// By: M9888
// Due: 11/11/2019

// File: SelectToEditAddressForm.cs
// This forms allows users to select one of the addresses so they may edit it

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPVApp
{
    public partial class EditAddressForm : Form
    {
        private List<Address> _addresses; //list of addresses

        // Precondition:  none
        // Postcondition: Creates the form and populates it with all the addresses from UPV
        public EditAddressForm(List<Address> addresses)
        {
            InitializeComponent();

            _addresses = addresses;

            // populates the list box with addresses from UPV
            foreach(Address address in _addresses)
            {
                listBoxSelectAddress.Items.Add($"{address.Name}: {address.Address1}");
            }
        }

        internal int AddressIndex
        {
            // Precondition:  None
            // Postcondition: The index of form's selected address combo box has been returned
            get
            {
                return listBoxSelectAddress.SelectedIndex;
            }
        }


        // Precondition:  Focus shifting from one of the address combo boxes
        //                sender is ComboBox
        // Postcondition: If no address selected, focus remains and error provider
        //                highlights the field
        private void listBoxSelectAddress_Validating(object sender, CancelEventArgs e)
        {
            // Downcast to sender as ComboBox, so make sure you obey precondition!
            ListBox lstBo = sender as ListBox; // Cast sender as combo box

            bool valid = true; // bool for testing

            if (listBoxSelectAddress.SelectedIndex == -1)
            {
                e.Cancel = true; // Stops focus changing process
                                 // Will NOT proceed to Validated event

                valid = false;
            }
            else lstBo = listBoxSelectAddress;

            if (!valid) // error message if not valid
            {
                e.Cancel = true; // Stops focus changing process
                                 // Will NOT proceed to Validated event
                NoAddressError.SetError(listBoxSelectAddress, "You must select an address.");
                MessageBox.Show("You must select an address.","Error");
            }
        }

        // Precondition:  Validating of sender not cancelled, so data OK
        //                sender is Control
        // Postcondition: Error provider cleared and focus allowed to change
        private void listBoxSelectAddress_Validated(object sender, EventArgs e)
        {
            // Downcast to sender as Control, so make sure you obey precondition!
            Control control = sender as Control; // Cast sender as Control
                                                 // Should always be a Control
            NoAddressError.SetError(control, "");
        }

        // Precondition:  User pressed on cancelBtn
        // Postcondition: Form closes
        private void cancelBtn_MouseDown(object sender, MouseEventArgs e)
        {
            // This handler uses MouseDown instead of Click event because
            // Click won't be allowed if other field's validation fails

            if (e.Button == MouseButtons.Left) // Was it a left-click?
                this.DialogResult = DialogResult.Cancel;
        }

        // Precondition:  User clicked on okBtn
        // Postcondition: If invalid field on dialog, keep form open and give first invalid
        //                field the focus. Else return OK and close form.
        private void okBtn_Click(object sender, EventArgs e)
        {
            // The easy way
            // Raise validating event for all enabled controls on form
            // If all pass, ValidateChildren() will be true
            if (ValidateChildren())
                this.DialogResult = DialogResult.OK;
        }
    }
}
