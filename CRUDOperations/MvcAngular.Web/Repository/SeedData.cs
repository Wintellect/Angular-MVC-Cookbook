using System;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MvcAngular.Web.Repository
{
    public class SeedData
    {
        public static void Seed(ExampleDbContext ctx)
        {
            if (!ctx.People.Any())
            {
                LoadPeopleData(ctx.Database.Connection.ConnectionString);
            }
            else
            {
                LogMsg("People records have already been added to the database.");
            }
        }

        private static void LoadPeopleData(string connStr)
        {
            const string peopleXmlFile = "People.xml";

            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../Repository");
            string fileName = Path.Combine(basePath, peopleXmlFile);
            var xdoc = XDocument.Load(fileName);

            DateTime startTime = DateTime.Now;
            int recordCount = 0, totalRecordCount = xdoc.Element("people").Elements("person").Count();
            LogMsg("Adding {0:n0} people records.", totalRecordCount);

            LogMsg("Opening database: {0}", connStr);
            using (var sqlConn = new SqlCeConnection(connStr))
            using (
                TableHelper peopleTable = new TableHelper("People"),
                postalTable = new TableHelper("Postal"),
                phoneTable = new TableHelper("Phone"),
                emailTable = new TableHelper("Email"))
            {
                sqlConn.Open();

                peopleTable.Open(sqlConn);
                postalTable.Open(sqlConn);
                phoneTable.Open(sqlConn);
                emailTable.Open(sqlConn);

                var peopleElements = xdoc.Element("people").Elements("person");
                foreach (var personElement in peopleElements)
                {
                    var person =
                        personElement
                            .Elements("name")
                            .Select(
                                name =>
                                new Person
                                    {
                                        Title = (string)name.Attribute("title"),
                                        FirstName = (string)name.Attribute("first"),
                                        MiddleName = (string)name.Attribute("middle"),
                                        LastName = (string)name.Attribute("last"),
                                        Suffix = (string)name.Attribute("suffix"),
                                    })
                            .Single();

                    person.PostalAddresses =
                        personElement
                            .Elements("address")
                            .Select(
                                addr =>
                                new PostalAddress
                                    {
                                        LineOne = (string)addr.Attribute("addr1"),
                                        LineTwo = (string)addr.Attribute("addr2"),
                                        City = (string)addr.Attribute("city"),
                                        StateProvince = (string)addr.Attribute("stateProv"),
                                        Country = (string)addr.Attribute("country"),
                                        PostalCode = (string)addr.Attribute("postal"),
                                    })
                            .ToList();

                    person.EmailAddresses =
                        personElement
                            .Elements("email")
                            .Select(
                                email =>
                                new EmailAddress
                                    {
                                        Address = (string)email.Attribute("addr"),
                                    })
                            .ToList();

                    person.PhoneNumbers =
                        personElement
                            .Elements("phone")
                            .Select(
                                phone =>
                                new PhoneNumber
                                    {
                                        Number = (string)phone.Attribute("num"),
                                        NumberType = (string)phone.Attribute("type"),
                                    })
                            .ToList();

                    peopleTable.Record.SetValue(1, person.Title);
                    peopleTable.Record.SetValue(2, person.FirstName);
                    peopleTable.Record.SetValue(3, person.MiddleName);
                    peopleTable.Record.SetValue(4, person.LastName);
                    peopleTable.Record.SetValue(5, person.Suffix);
                    person.PersonId = peopleTable.Insert();

                    foreach (var postal in person.PostalAddresses)
                    {
                        postal.PersonId = person.PersonId;
                        postalTable.Record.SetValue(1, postal.PersonId);
                        postalTable.Record.SetValue(2, postal.LineOne);
                        postalTable.Record.SetValue(3, postal.LineTwo);
                        postalTable.Record.SetValue(4, postal.City);
                        postalTable.Record.SetValue(5, postal.StateProvince);
                        postalTable.Record.SetValue(6, postal.Country);
                        postalTable.Record.SetValue(7, postal.PostalCode);
                        postal.PostalAddressId = postalTable.Insert();
                    }

                    foreach (var phone in person.PhoneNumbers)
                    {
                        phone.PersonId = person.PersonId;
                        phoneTable.Record.SetValue(1, phone.PersonId);
                        phoneTable.Record.SetValue(2, phone.Number);
                        phoneTable.Record.SetValue(3, phone.NumberType);
                        phone.PhoneNumberId = phoneTable.Insert();
                    }

                    foreach (var email in person.EmailAddresses)
                    {
                        email.PersonId = person.PersonId;
                        emailTable.Record.SetValue(1, email.PersonId);
                        emailTable.Record.SetValue(2, email.Address);
                        email.EmailAddressId = emailTable.Insert();
                    }

                    if (++recordCount % 100 == 0)
                    {
                        LogMsg("Added {0:n0} records ({1}%)", recordCount, recordCount * 100 / totalRecordCount);
                    }
                }
            }

            LogMsg("Finished, added {0:n0} people records.", recordCount);
            LogMsg("Time: {0} ({1:n2} recs/sec)",
                DateTime.Now.Subtract(startTime),
                recordCount / DateTime.Now.Subtract(startTime).TotalSeconds);
        }

        private static void LogMsg(string msgFmt, params object[] msgArgs)
        {
            Console.WriteLine(msgFmt, msgArgs);
        }

        private class TableHelper : IDisposable
        {
            private readonly string _tableName;
            private SqlCeCommand _sqlCmd;
            private SqlCeResultSet _resultSet;
            private SqlCeUpdatableRecord _record;

            public TableHelper(string tableName)
            {
                _tableName = tableName;
            }

            public SqlCeResultSet ResultSet
            {
                get { return _resultSet; }
            }

            public SqlCeUpdatableRecord Record
            {
                get { return _record; }
            }

            public void Open(SqlCeConnection sqlConn)
            {
                _sqlCmd = sqlConn.CreateCommand();
                _sqlCmd.CommandText = String.Concat("SELECT * FROM ", _tableName);
                _resultSet = _sqlCmd.ExecuteResultSet(ResultSetOptions.Updatable | ResultSetOptions.Scrollable);
                _record = _resultSet.CreateRecord();
            }

            public int Insert()
            {
                _resultSet.Insert(_record, DbInsertOptions.PositionOnInsertedRow);
                return _resultSet.GetInt32(0);
            }

            public void Dispose()
            {
                if (_sqlCmd != null)
                {
                    _resultSet.Dispose();
                    _sqlCmd.Dispose();
                    _record = null;
                    _resultSet = null;
                    _sqlCmd = null;
                }
            }
        }
    }
}