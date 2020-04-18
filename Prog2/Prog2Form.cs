// Program 3
// CIS 200-01
// By: M9888
// Due: 11/11/2019

// File: SelectToEditAddressForm.cs
// This class creates the main GUI for Program 2. It provides a
// File menu with About and Exit items, an Insert menu with Address and
// Letter items, and a Report menu with List Addresses and List Parcels
// items.
// UPDATE: added the ability to open and save the file
// UPDATE: added the ability to edit addresses

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; //filestream
using System.Linq;
using System.Runtime.Serialization; //SerializationException
using System.Runtime.Serialization.Formatters.Binary; //BinaryFormatter
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace UPVApp
{
    [Serializable]
    public partial class Prog2Form : Form
    {
        private UserParcelView upv; // The UserParcelView

        // object for serializing upv in binary format
        private readonly BinaryFormatter formatter = new BinaryFormatter();// Object for serializing library object in binary format 
        private FileStream saveFile;// stream for writing to a file

        // object for deserializing upv in binary format
        private readonly BinaryFormatter reader = new BinaryFormatter();// Object for deserializing library object in binary format 
        private FileStream openFile;// stream for reading from a file

        // Precondition:  None
        // Postcondition: The form's GUI is prepared for display. A few test addresses are
        //                added to the list of addresses
        public Prog2Form()
        {
            InitializeComponent();

            upv = new UserParcelView(); // 

            // Test Data - Magic Numbers OK
            //upv.AddAddress("  John Smith  ", "   123 Any St.   ", "  Apt. 45 ", "  Louisville   ", "  KY   ", 40202); // Test Address 1
            //upv.AddAddress("Jane Doe", "987 Main St.", "Beverly Hills", "CA", 90210); // Test Address 2
            //upv.AddAddress("James Kirk", "654 Roddenberry Way", "Suite 321", "El Paso", "TX", 79901); // Test Address 3
            //upv.AddAddress("John Crichton", "678 Pau Place", "Apt. 7", "Portland", "ME", 04101); // Test Address 4
            //upv.AddAddress("John Doe", "111 Market St.", "", "Jeffersonville", "IN", 47130); // Test Address 5
            //upv.AddAddress("Jane Smith", "55 Hollywood Blvd.", "Apt. 9", "Los Angeles", "CA", 90212); // Test Address 6
            //upv.AddAddress("Captain Robert Crunch", "21 Cereal Rd.", "Room 987", "Bethesda", "MD", 20810); // Test Address 7
            //upv.AddAddress("Vlad Dracula", "6543 Vampire Way", "Apt. 1", "Bloodsucker City", "TN", 37210); // Test Address 8

            //upv.AddLetter(upv.AddressAt(0), upv.AddressAt(1), 3.95M);                     // Letter test object
            //upv.AddLetter(upv.AddressAt(2), upv.AddressAt(3), 4.25M);                     // Letter test object
            //upv.AddGroundPackage(upv.AddressAt(4), upv.AddressAt(5), 14, 10, 5, 12.5);    // Ground test object
            //upv.AddGroundPackage(upv.AddressAt(6), upv.AddressAt(7), 8.5, 9.5, 6.5, 2.5); // Ground test object
            //upv.AddNextDayAirPackage(upv.AddressAt(0), upv.AddressAt(2), 25, 15, 15, 85, 7.50M);     // Next Day test object
            //upv.AddNextDayAirPackage(upv.AddressAt(2), upv.AddressAt(4), 9.5, 6.0, 5.5, 5.25, 5.25M);  // Next Day test object
            //upv.AddNextDayAirPackage(upv.AddressAt(1), upv.AddressAt(6), 10.5, 6.5, 9.5, 15.5, 5.00M);  // Next Day test object
            //upv.AddTwoDayAirPackage(upv.AddressAt(4), upv.AddressAt(6), 46.5, 39.5, 28.0, 80.5, TwoDayAirPackage.Delivery.Saver); // Two Day test object
            //upv.AddTwoDayAirPackage(upv.AddressAt(7), upv.AddressAt(0), 15.0, 9.5, 6.5, 75.5, TwoDayAirPackage.Delivery.Early);  // Two Day test object
            //upv.AddTwoDayAirPackage(upv.AddressAt(5), upv.AddressAt(3), 12.0, 12.0, 6.0, 5.5, TwoDayAirPackage.Delivery.Saver);  // Two Day test object

        }

        // Precondition:  File, About menu item activated
        // Postcondition: Information about author displayed in dialog box
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NL = Environment.NewLine; // Newline shorthand

            MessageBox.Show($"Program 3{NL}By: M9888{NL}CIS 200{NL}Fall 2019", "About Program 3");
        }

        // Precondition:  File, Exit menu item activated
        // Postcondition: The application is exited
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Precondition:  Insert, Address menu item activated
        // Postcondition: The Address dialog box is displayed. If data entered
        //                are OK, an Address is created and added to the list
        //                of addresses
        private void addressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddressForm addressForm = new AddressForm();    // The address dialog box form
            DialogResult result = addressForm.ShowDialog(); // Show form as dialog and store result
            int zip; // Address zip code

            if (result == DialogResult.OK) // Only add if OK
            {
                if (int.TryParse(addressForm.ZipText, out zip))
                {
                    upv.AddAddress(addressForm.AddressName, addressForm.Address1,
                        addressForm.Address2, addressForm.City, addressForm.State,
                        zip); // Use form's properties to create address
                }
                else // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Address Validation!", "Validation Error");
                }
            }

            addressForm.Dispose(); // Best practice for dialog boxes
                                   // Alternatively, use with using clause as in Ch. 17
        }

        // Precondition:  Report, List Addresses menu item activated
        // Postcondition: The list of addresses is displayed in the addressResultsTxt
        //                text box
        private void listAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String
            string NL = Environment.NewLine;            // Newline shorthand

            result.Append("Addresses:");
            result.Append(NL); // Remember, \n doesn't always work in GUIs
            result.Append(NL);

            foreach (Address a in upv.AddressList)
            {
                result.Append(a.ToString());
                result.Append(NL);
                result.Append("------------------------------");
                result.Append(NL);
            }

            reportTxt.Text = result.ToString();

            // -- OR --
            // Not using StringBuilder, just use TextBox directly

            //reportTxt.Clear();
            //reportTxt.AppendText("Addresses:");
            //reportTxt.AppendText(NL); // Remember, \n doesn't always work in GUIs
            //reportTxt.AppendText(NL);

            //foreach (Address a in upv.AddressList)
            //{
            //    reportTxt.AppendText(a.ToString());
            //    reportTxt.AppendText(NL);
            //    reportTxt.AppendText("------------------------------");
            //    reportTxt.AppendText(NL);
            //}

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        // Precondition:  Insert, Letter menu item activated
        // Postcondition: The Letter dialog box is displayed. If data entered
        //                are OK, a Letter is created and added to the list
        //                of parcels
        private void letterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LetterForm letterForm; // The letter dialog box form
            DialogResult result;   // The result of showing form as dialog
            decimal fixedCost;     // The letter's cost

            if (upv.AddressCount < LetterForm.MIN_ADDRESSES) // Make sure we have enough addresses
            {
                MessageBox.Show("Need " + LetterForm.MIN_ADDRESSES + " addresses to create letter!",
                    "Addresses Error");
                return; // Exit now since can't create valid letter
            }

            letterForm = new LetterForm(upv.AddressList); // Send list of addresses
            result = letterForm.ShowDialog();

            if (result == DialogResult.OK) // Only add if OK
            {
                if (decimal.TryParse(letterForm.FixedCostText, out fixedCost))
                {
                    // For this to work, LetterForm's combo boxes need to be in same
                    // order as upv's AddressList
                    upv.AddLetter(upv.AddressAt(letterForm.OriginAddressIndex),
                        upv.AddressAt(letterForm.DestinationAddressIndex),
                        fixedCost); // Letter to be inserted
                }
                else // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Letter Validation!", "Validation Error");
                }
            }

            letterForm.Dispose(); // Best practice for dialog boxes
                                  // Alternatively, use with using clause as in Ch. 17
        }

        // Precondition:  Report, List Parcels menu item activated
        // Postcondition: The list of parcels is displayed in the parcelResultsTxt
        //                text box
        private void listParcelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String
            decimal totalCost = 0;                      // Running total of parcel shipping costs
            string NL = Environment.NewLine;            // Newline shorthand

            result.Append("Parcels:");
            result.Append(NL); // Remember, \n doesn't always work in GUIs
            result.Append(NL);

            foreach (Parcel p in upv.ParcelList)
            {
                result.Append(p.ToString());
                result.Append(NL);
                result.Append("------------------------------");
                result.Append(NL);
                totalCost += p.CalcCost();
            }

            result.Append(NL);
            result.Append($"Total Cost: {totalCost:C}");

            reportTxt.Text = result.ToString();

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }




        // ----------------------------------------------------------------------- OPEN --------->>

        // Precondition:  none
        // Postcondition: Specified file in file explorer is opened and a message is given to the user
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            // create and show dialog box enabling user to open file
            DialogResult result; // result of OpenFileDialog
            string fileName; // name of file containing data

            using (OpenFileDialog fileChooser = new OpenFileDialog())
            {
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; // get specified name
            }

            // Error Checking
            // ensure that user clicked "OK"
            if (result == DialogResult.OK)
            {

                if (string.IsNullOrWhiteSpace(fileName)) // show error if user specified invalid file
                {
                    MessageBox.Show("Invalid File Name", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        openFile = new FileStream(fileName, FileMode.Open, FileAccess.Read); // opens and plans to read the file
                        upv = (UserParcelView)reader.Deserialize(openFile); // deserializes the file

                        reportTxt.Text = $"File {fileName} has been successfully loaded."; //feedback
                    }
                    catch (DirectoryNotFoundException)
                    {
                        MessageBox.Show("Error saving file \n DirectoryNotFoundException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // DirectoryNotFoundException
                    }
                    catch (EndOfStreamException)
                    {
                        MessageBox.Show("Error saving file \n EndOfStreamException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // EndOfStreamException
                    }
                    catch (FileLoadException)
                    {
                        MessageBox.Show("Error saving file \n FileLoadException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //FileLoadException
                    }
                    catch (FileNotFoundException)
                    {
                        MessageBox.Show("Error saving file \n FileNotFoundException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //FileNotFoundException
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show("Error saving file \n UnauthorizedAccessException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //UnauthorizedAccessException
                    }
                    catch (IOException) //catches IOException
                    {
                        MessageBox.Show("Error Opening File \n IOException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //IOException
                    }
                    catch (SerializationException) //catches SerializationException
                    {
                        openFile.Close();
                        MessageBox.Show("Error Opening File \n SerializationException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //SerializationException
                    }
                }
            }
        }




        // ----------------------------------------------------------------------- SAVE --------->>

        // Precondition:  none
        // Postcondition: The program file is saved and a message is given to the user
        private void BtnSaveAs_Click(object sender, EventArgs e)
        {
            // create and show dialog box enabling user to save file
            DialogResult result;
            string fileName; // name of file to save data

            using (SaveFileDialog fileChooser = new SaveFileDialog())
            {
                fileChooser.CheckFileExists = false; // let user create file

                // retrieve the result of the dialog box
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; // get specified file name
            }

            // ensure that user clicked "OK"
            if (result == DialogResult.OK)
            {
                // show error if user specified invalid file
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show("Invalid File Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //feedback
                }
                else
                {
                    // save file via FileStream if user specified valid file
                    try
                    {
                        // open file with write access
                        saveFile = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                        formatter.Serialize(saveFile, upv);
                        saveFile.Close(); //close file after saving so we may access it later after changing files (otherwise it's still considered in use)
                        reportTxt.Text = $"File {fileName} has been successfully saved."; //feedback

                    }
                    catch (DirectoryNotFoundException)
                    {
                        MessageBox.Show("Error saving file \n DirectoryNotFoundException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //DirectoryNotFoundException
                    }
                    catch (EndOfStreamException)
                    {
                        MessageBox.Show("Error saving file \n EndOfStreamException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //EndOfStreamException
                    }
                    catch (FileLoadException)
                    {
                        MessageBox.Show("Error saving file \n FileLoadException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //FileLoadException
                    }
                    catch (FileNotFoundException)
                    {
                        MessageBox.Show("Error saving file \n FileNotFoundException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //FileNotFoundException
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show("Error saving file \n UnauthorizedAccessException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //UnauthorizedAccessException
                    }
                    catch (IOException) // catches all IOException
                    {
                        // notify user if file could not be opened
                        MessageBox.Show("Error saving file \n IOException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //IOException
                    }
                    catch (SerializationException) //catches SerializationException
                    {
                        saveFile.Close();
                        MessageBox.Show("Error Opening File \n SerializationException", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //SerializationException
                    }
                }
            }
        }

        // ----------------------------------------------------------------------- EDIT --------->>

        // Precondition:  none
        // Postcondition: Specified address is resubmitted with the new details
        private void BtnEditAddress_Click(object sender, EventArgs e)
        {
            List<Address> addresses = upv.AddressList; // creates a list of addresses
            int index;  // used to identfy address 

            if (upv.AddressCount <= 0) { MessageBox.Show("There must be atleast one address to edit.", "Error"); } //checks whether there is atleast one address
            else
            {
                EditAddressForm selectAddress = new EditAddressForm(addresses);    // The address dialog box form
                DialogResult result = selectAddress.ShowDialog(); // Show form as dialog and store result

                if (result == DialogResult.OK) 
                {
                    index = selectAddress.AddressIndex; // keeps track of index

                    Address selectedAddress = addresses[index]; // the index of the selcted address is kept
                    AddressForm editAddress = new AddressForm(); // creates new dialogue to edit the address

                    // views the address details and presenets to user
                    editAddress.AddressName = selectedAddress.Name;
                    editAddress.Address1 = selectedAddress.Address1;
                    editAddress.Address2 = selectedAddress.Address2;
                    editAddress.City = selectedAddress.City;
                    editAddress.State = selectedAddress.State;
                    editAddress.ZipText = selectedAddress.Zip.ToString();

                    DialogResult result_2 = editAddress.ShowDialog();

                    // edits the address details
                    if (result_2 == DialogResult.OK)
                    {
                        selectedAddress.Name = editAddress.AddressName;
                        selectedAddress.Address1 = editAddress.Address1;
                        selectedAddress.Address2 = editAddress.Address2;
                        selectedAddress.City = editAddress.City;
                        selectedAddress.State = editAddress.State;
                        selectedAddress.Zip = Convert.ToInt32(editAddress.ZipText);

                        reportTxt.Text = $"Address has been successfully edited."; //feedback
                    }
                }
            }
        }
    }
}