// Program 0
// CIS 200-01
// Fall 2019
// Due: 9/9/2019
// By: Andrew L. Wright (students use Grading ID)

// File: Address.cs
// This classes stores a typical US address consisting of name,
// two address lines, city, state, and 5 digit zip code.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class Address
{
    public const int MIN_ZIP = 0;     // Minimum ZipCode value
    public const int MAX_ZIP = 99999; // Maximum ZipCode value

    private string _name;     // Address' name
    private string _address1; // First address line
    private string _address2; // Second address line, optional
    private string _city;     // Address' city
    private string _state;    // Address' state
    private int _zip;         // Address' zip code

    // Precondition:  MIN_ZIP <= zipcode <= MAX_ZIP
    // Postcondition: The address is created with the specified values for
    //                name, address1, address2, city, state, and zipcode
    public Address(String name, String address1, String address2,
        String city, String state, int zipcode)
    {
        Name = name;
        Address1 = address1;
        Address2 = address2;
        City = city;
        State = state;
        Zip = zipcode;
    }

    // Precondition:  MIN_ZIP <= zipcode <= MAX_ZIP
    // Postcondition: The address is created with the specified values for
    //                name, address1, city, state, and zipcode
    public Address(String name, String address1, String city,
        String state, int zipcode) :
        this(name, address1, string.Empty, city, state, zipcode)
    {
        // No body needed
        // Calls previous constructor sending empty string for Address2
    }

    public String Name
    {
        // Precondition:  None
        // Postcondition: The address' name has been returned
        get
        {
            return _name;
        }

        // Precondition:  value must not be empty
        // Postcondition: The address' name has been set to the
        //                specified value
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentOutOfRangeException("Name",
                    value, "Name must not be empty");
            else
                _name = value.Trim();
        }
    }

    public String Address1
    {
        // Precondition:  None
        // Postcondition: The address' first address line has been returned
        get
        {
            return _address1;
        }

        // Precondition:  value must not be empty
        // Postcondition: The address' first address line has been set to
        //                the specified value
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentOutOfRangeException("Address1",
                    value, "Address1 must not be empty");
            else
                _address1 = value.Trim();
        }
    }

    public String Address2
    {
        // Precondition:  None
        // Postcondition: The address' second address line has been returned
        get
        {
            return _address2;
        }

        // Precondition:  None
        // Postcondition: The address' second address line has been set to
        //                the specified value
        set
        {
                _address2 = value.Trim();
        }
    }

    public String City
    {
        // Precondition:  None
        // Postcondition: The address' city has been returned
        get
        {
            return _city;
        }

        // Precondition:  value must not be empty
        // Postcondition: The address' city has been set to the
        //                specified value
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentOutOfRangeException("City",
                    value, "City must not be empty");
            else
                _city = value.Trim();
        }
    }

    public String State
    {
        // Precondition:  None
        // Postcondition: The address' state has been returned
        get
        {
            return _state;
        }

        // Precondition:  value must not be empty
        // Postcondition: The address' state has been set to the
        //                specified value
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentOutOfRangeException("State",
                    value, "State must not be empty");
            else
                _state = value.Trim();
        }
    }

    public int Zip
    {
        // Precondition:  None
        // Postcondition: The address' zip code has been returned
        get
        {
            return _zip;
        }

        // Precondition:  MIN_ZIP <= value <= MAX_ZIP
        // Postcondition: The address' zip code has been set to the
        //                specified value
        set
        {
            if ((value >= MIN_ZIP) && (value <= MAX_ZIP))
                _zip = value;
            else
                throw new ArgumentOutOfRangeException("Zip", value,
                    "Zip must be U.S. 5 digit zip code");
        }
    }

    // Precondition:  None
    // Postcondition: A String with the address' data has been returned
    public override String ToString()
    {
        string NL = Environment.NewLine; // NewLine shortcut
        string result;                   // Builds formatted string

        result = $"{Name}{NL}{Address1}{NL}";

        if (!String.IsNullOrWhiteSpace(Address2)) // Is Address2 not empty?
            result += $"{Address2}{NL}";

        result += $"{City}, {State} {Zip:D5}";

        // -- OR --
        // Compact Way
        //result = $"{Name}{NL}{Address1}{NL}{Address2}" +
        //    $"{(String.IsNullOrWhiteSpace(Address2) ? string.Empty : NL)}" +
        //    $"{City}, {State} {Zip:D5}";

        return result;
    }
}