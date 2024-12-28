// See https://aka.ms/new-console-template for more information
using SMNDotNetBatch5.ConsoleApp;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

//Adodotnetexample ado=new Adodotnetexample();
//ado.Read();
//ado.Create();
//ado.Updat();
//ado.Delete();

//DapperExample dpexample=new DapperExample();
//dpexample.Read();
//dpexample.Creat("Christmax", "William", "Merry Christmas");
//dpexample.Update(8, "Sweet", "Saint", "Sweet Moments");
//dpexample.Delete(8, "Sweet", "Saint", "Sweet Moments");

EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.Read();
eFCoreExample.Create("Forever", "NineOne", "Forever Young");