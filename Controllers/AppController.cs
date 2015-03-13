using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Security.AccessControl;
using System.Web.Mvc;
//using EnvDTE;
//using EnvDTE80;
//using Microsoft.VisualStudio.TextTemplating;
//using Microsoft.VisualStudio.TextTemplating.VSHost;
//using Microsoft.VisualStudio.Shell;
using System.Web.WebPages;
using System.Xml;
using System.Xml.Linq;
using Microsoft.CSharp;
//using Microsoft.Owin;
//using Owin;

namespace AngularJSProofofConcept.Controllers
{
    public class AppCController : Controller
    {
        //internal SVsServiceProvider ServiceProvider;
        //private SqlDependency _dependency = null;

        private static readonly List<object> List = new List<object>();

        public ActionResult Api() 
        {
            if (List.Count != 0)
            {
                return Json(List, JsonRequestBehavior.AllowGet);
            }
            List.Add(new
            {
                EventName = "Event1"
            });
            List.Add(new
            {
                EventName = "Event2"
            });
            List.Add(new
            {
                EventName = "Event3"
            });
            return Json(List, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            //DbEnum db = new DbEnum("test");
            //string pageContent = db.TransformText();
            ////string path = Path.GetFullPath("DbEnum.tt");
            //System.IO.File.WriteAllText(@"C:\Users\jtorres\Documents\Visual Studio 2012\Projects\AngularJSProofofConcept\outputPageEnum.cs", pageContent);

            //var myhast = new Hashtable();
            //myhast.Add("test", 1);
            //UpdateResourceFile(myhast, @"C:\Users\jtorres\Documents\Visual Studio 2012\Projects\AngularJSProofofConcept\Resource1.resx");

            //Configuration configManager = ConfigurationManager.OpenExeConfiguration(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "A.config"));
            //KeyValueConfigurationCollection confCollection = configManager.AppSettings.Settings;
            //confCollection.Add("test", "test");
            //configManager.Save(ConfigurationSaveMode.Modified);
            //ConfigurationManager.RefreshSection(configManager.AppSettings.SectionInformation.Name);

            //var compiler = new CSharpCodeProvider();
            //var cparameters = new CompilerParameters
            //{
            //    GenerateInMemory = false,
            //    OutputAssembly = String.Format(@"{0}.dll",
            //        @"C:\Users\jtorres\Documents\Visual Studio 2012\Projects\AngularJSProofofConcept\bin\AngularJSProofofConcept")
            //};
            //var compilerResults = compiler.CompileAssemblyFromSource(cparameters, pageContent);

            //if (compilerResults.Errors.Count > 0)
            //{
            //    string errors = "";
            //}
            //else
            //{
            //    string noerrors = "";
            //}

            //var myDictionary = new MyDictionary();
            //myDictionary.MyDictionaryObject.Add("tes", "tesdt");


            //IServiceProvider hostServiceProvider = (IServiceProvider)HttpContext;
            //EnvDTE._DTE dte = (EnvDTE._DTE)hostServiceProvider.GetService(typeof(EnvDTE._DTE));

            //DTE2 dte = Package.GetGlobalService(typeof(DTE)) as DTE2;
            //EnvDTE80.DTE2 MyDte;
            //ICustomTypeDescriptor typeDescriptor = HttpContext as ICustomTypeDescriptor;

            //PropertyDescriptorCollection propertyCollection = typeDescriptor.GetProperties();



            //// first get the DTE object from the DataContext.

            //MyDte = propertyCollection.Find("DTE", false).GetValue(HttpContext) as DTE2;

            // Get a service provider – how you do this depends on the context:
            //IServiceProvider serviceProvider = new ServiceProvider(dte.DTE as Microsoft.VisualStudio.OLE.Interop.IServiceProvider); // An instance of EnvDTE, for example 
            //// Get the text template service:
            //ITextTemplating t4 = serviceProvider.GetService(typeof(STextTemplating)) as ITextTemplating;
            //ITextTemplatingSessionHost sessionHost = t4 as ITextTemplatingSessionHost;

            //// Create a Session in which to pass parameters:
            //sessionHost.Session = sessionHost.CreateSession();
            //sessionHost.Session["parameter1"] = "Hello";
            //sessionHost.Session["parameter2"] = DateTime.Now;

            //// Pass another value in CallContext:
            //System.Runtime.Remoting.Messaging.CallContext.LogicalSetData("parameter3", 42);

            //// Process a text template:
            //string result = t4.ProcessTemplate("",
            //    // This is the test template:
            //   "<#@parameter type=\"System.String\" name=\"parameter1\"#>"
            // + "<#@parameter type=\"System.DateTime\" name=\"parameter2\"#>"
            // + "<#@parameter type=\"System.Int32\" name=\"parameter3\"#>"
            // + "Test: <#=parameter1#>    <#=parameter2#>    <#=parameter3#>");

            //using (
            //    var connection =
            //        new SqlConnection(@"Data Source=(localdb)\v11.0;Initial Catalog=e2rm_dev;Integrated Security=true"))
            //{
            //    connection.Open();
            //    using (var command = new SqlCommand(
            //        @"SELECT [MessageID],[UserID],[MessageText],[IsSent] FROM [dbo].[Messages]",
            //        connection))
            //    {

            //        // Create a dependency and associate it with the SqlCommand.
            //        var _dependency = new SqlDependency(command);
            //        // Maintain the refence in a class member.

            //        // Subscribe to the SqlDependency event.
            //        _dependency.OnChange += OnDependencyChange;

            //        // Execute the command.
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            // Process the DataReader.
            //        }
            //    }
            //}

            return View();
        }

        //public static void UpdateResourceFile(Hashtable data, String path)
        //{
        //    Hashtable resourceEntries = new Hashtable();

        //    //Get existing resources
        //    ResXResourceReader reader = new ResXResourceReader(path);
        //    if (reader != null)
        //    {
        //        IDictionaryEnumerator id = reader.GetEnumerator();
        //        foreach (DictionaryEntry d in reader)
        //        {
        //            if (d.Value == null)
        //                resourceEntries.Add(d.Key.ToString(), "");
        //            else
        //                resourceEntries.Add(d.Key.ToString(), d.Value.ToString());
        //        }
        //        reader.Close();
        //    }

        //    //Modify resources here...
        //    foreach (String key in data.Keys)
        //    {
        //        if (!resourceEntries.ContainsKey(key))
        //        {

        //            String value = data[key].ToString();
        //            if (value == null) value = "";

        //            resourceEntries.Add(key, value);
        //        }
        //    }

        //    //Write the combined resource file
        //    ResXResourceWriter resourceWriter = new ResXResourceWriter(path);

        //    foreach (String key in resourceEntries.Keys)
        //    {
        //        resourceWriter.AddResource(key, resourceEntries[key]);
        //    }
        //    resourceWriter.Generate();
        //    resourceWriter.Close();

        //}

        //// Handler method
        //static void OnDependencyChange(object sender,
        //   SqlNotificationEventArgs e)
        //{
        //    // Handle the event (for example, invalidate this cache entry).
        //    var template = new DbEnum("string");
        //    template.TransformText();

        //    //_dependency.OnChange -= OnDependencyChange;

        //    //var compiler = new CSharpCodeProvider();
        //    //var cparameters = new CompilerParameters
        //    //{
        //    //    GenerateInMemory = true,
        //    //    OutputAssembly = String.Format(@"{0}\{1}.dll",
        //    //        System.Environment.CurrentDirectory,
        //    //        "DbEnum.cs".Replace(".", "_"))
        //    //};
        //    //var compilerResults = compiler.CompileAssemblyFromSource(cparameters, code);

        //    //if (compilerResults.Errors.Count > 0)
        //    //{
        //    //    string errors = "";
        //    //}
        //    //else
        //    //{
        //    //    string noerrors = "";
        //    //}
        //}
        
        public ActionResult RouteEvents()
        {
            return View();
        }

        public ActionResult RouteEvent(string Event)
        {
            return View(Event);
        }

        [HttpPost]
        public ActionResult InsertEvent(string name)
        {
            List.Add(new
            {
                EventName = name
            });
            return RedirectToAction("Index"); 
        }
  
    }

    //useful code from the visual studio plugin solution jira 2085
    //object stream;
    //using (var resourceReader = new ResourceReader(wszInputFilePath))
    //{
    //    int arraySplitLength = wszInputFilePath.Split(new[] { '\\' }).Length;
    //    {
    //        var items = resourceReader.OfType<DictionaryEntry>();
    //        stream = items.First(x =>
    //        {
    //            var s = x.Key as string;
    //            return s != null && s.Contains(wszInputFilePath.Split(new[] { '\\' })[arraySplitLength - 1].Split(new[] { '.' })[0].ToLower());
    //        }).Value;
    //    }
    //}

    //using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(resourceFileContents)))
    //{
    //    var reader = new ResXResourceReader(stream);
    //    IDictionaryEnumerator master = reader.GetEnumerator();

    //    while (master.MoveNext())
    //    {
    //        foreach (KeyValuePair<string, string> kvp in customListFromDb)
    //        {
    //            if (kvp.Key == (string)master.Key)
    //            {
    //                listResult.Append("[").Append(kvp.Key).Append(",").Append(kvp.Value).Append("]:");
    //            }
    //            else
    //            {
    //                listResult.Append("[").Append(master.Key).Append(",").Append(master.Value).Append("]:");
    //            }
    //        }
    //    }

    //    return listResult.ToString().Remove(listResult.Length - 1, 1);
    //}

    //var properties = new ResxParser().Parse(bstrInputFileContents);

    //var formatter = new CodeFormatter().Format(properties);

    //formatter.ToString();


    //            var assembly = Assembly.GetExecutingAssembly();
    //            var strResources = assembly.GetName().Name + ".g.resources";
    //            var rStream = assembly.GetManifestResourceStream(strResources);
    //            ResourceReader resourceReader = null;
    //            if (rStream != null)
    //            {
    //                resourceReader = new ResourceReader(rStream);
    //            }
    //            object stream = null;
    //            if (resourceReader != null)
    //            {
    //                var items = resourceReader.OfType<DictionaryEntry>();
    //                stream = items.First(x =>
    //                {
    //                    var s = x.Key as string;
    //                    return s != null && s.Contains("Resource1.resx".ToLower());
    //                }).Value;
    //            }

    //            XDocument myXDocument = null;
    //            if (stream != null)
    //            {
    //                using (var reader = new StreamReader((Stream)stream))
    //                {
    //                    myXDocument = XDocument.Load(reader);
    //                }
    //            }

    //            IEnumerable<XElement> result = null;

    //            if (myXDocument != null)
    //            {
    //                var xElement = myXDocument.Element("root");
    //                if (xElement != null)
    //                {
    //                    // ReSharper disable once SuspiciousTypeConversion.Global
    //                    result = from el in xElement.Elements()
    //                             where el.Name == "data"
    //                             select el;
    //                }
    //            }

    //            var res = new StringBuilder();
    //            var xElements = result as IList<XElement> ?? result.ToList();
    //            IEnumerator<XElement> enume = xElements.GetEnumerator();
    //            while (enume.MoveNext())
    //            {
    //                res.Append(@"internal static string ").Append(enume.Current.FirstAttribute.Value).Append(@"{
    //                                get {
    //                                    return ResourceManager.GetString(""").Append(enume.Current.FirstAttribute.Value).Append(@""", resourceCulture);
    //                                }
    //                            }").Append(Environment.NewLine);
    //            }

}
