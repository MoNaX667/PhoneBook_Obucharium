// <copyright file="Error.cs" company="Some Company">
// Copyright (c) Sprocket Enterprises. All rights reserved.
// </copyright>
// <author>Vitalit Belyakov</author>

namespace PhoneBook
{
    /// <summary>
    /// Error enum
    /// </summary>
    internal enum Error
    {
        /// <summary>
        /// Wrong id
        /// </summary>
        WrongId,

        /// <summary>
        /// Wrong phone number
        /// </summary>
        WrongPhoneNumberFormat,

        /// <summary>
        /// File not found
        /// </summary>
        FileNotFound,

        /// <summary>
        /// None error
        /// </summary>
        None,
    }
}
