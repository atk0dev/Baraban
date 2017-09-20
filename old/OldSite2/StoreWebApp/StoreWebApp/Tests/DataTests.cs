// ***********************************************************************
// Assembly         : ShowcaseED
// Author           : Andrii Tkach
// Created          : 11-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 11-12-2012
// ***********************************************************************
// <copyright file="DataTests.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    
// </summary>
// ***********************************************************************

namespace ShowcaseED.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using NUnit.Framework;
    using ShowcaseED.Data;
    using ShowcaseED.Interfaces;
    using ShowcaseED.Model;

    /// <summary>
    /// Data Tests
    /// </summary>
    [TestFixture]
    public class DataTests
    {
        /// <summary>
        /// The db conn string
        /// </summary>
        private string databaseConnString = "server=localhost;user id=root;password=androot;persist security info=True;database=mj_bikeshop";

        /// <summary>
        /// Olds the data provider_ load all media_ operation result.
        /// </summary>
        [Test]
        public void OldDataProvider_LoadAllMedia_OperationResult()
        {
            IStoreDataSource ds = new StoreDataProvider(this.databaseConnString);
            List<MediaDraft> list = ds.LoadAllMedia().ToList();
            
            Assert.IsNotNull(list);
        }

        /// <summary>
        /// Dumpeds the data provider_ load all media_ result.
        /// </summary>
        [Test]
        public void DumpedDataProvider_LoadAllMedia_Result()
        {
            var list = new DumpedDataProvider(this.databaseConnString, string.Empty).LoadMedia(0, 0, 10000);
            Assert.IsNotNull(list);
        }
    }
}