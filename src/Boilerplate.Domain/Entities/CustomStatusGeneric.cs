
using StatusGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Entities;

public class CustomStatusGeneric
{
    //
    // Summary:
    //     This is true if there are no errors registered
    public bool IsValid { get; set;}
    //
    // Summary:
    //     This is true if any errors have been added
    public bool HasErrors { get; set;}
    //
    // Summary:
    //     On success this returns any message set by GenericServices, or any method that
    //     returns a status If there are errors it contains the message "Failed with NN
    //     errors"
    public string? Message { get; set; }
}