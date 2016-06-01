// <copyright file="Commands.cs" company="Some Company">
// Copyright (c) Sprocket Enterprises. All rights reserved.
// </copyright>
// <author>Vitalit Belyakov</author>

namespace PhoneBook
{
    /// <summary>
    /// Commands list
    /// </summary>
    internal enum Commands
    {
        /// <summary>
        /// Next page
        /// </summary>
        NextPage,

        /// <summary>
        /// Old page
        /// </summary>
        PrevioslyPage,

        /// <summary>
        /// Search items
        /// </summary>
        Search,

        /// <summary>
        /// Sort phone book
        /// </summary>
        Sort,

        /// <summary>
        /// Add new contact in phone book
        /// </summary>
        Add,

        /// <summary>
        /// Delete by id
        /// </summary>
        DeleteById,

        /// <summary>
        /// Exit and save changed
        /// </summary>
        ExitAndSaveChanged,

        /// <summary>
        /// Return to start page
        /// </summary>
        ReturnToStartPage,

        /// <summary>
        /// Create test list
        /// </summary>
        CreateTestList,

        /// <summary>
        /// Clear list
        /// </summary>
        ClearList,
    }
}
