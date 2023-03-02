﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Implementations;

public interface IAwsS3Configuration
{
    string AwsAccessKey { get; set; }
    string AwsSecretAccessKey { get; set; }
    string AwsSessionToken { get; set; }
    string BucketName { get; set; }
    string BucketFolder { get; set; }
    string Region { get; set; }
}
