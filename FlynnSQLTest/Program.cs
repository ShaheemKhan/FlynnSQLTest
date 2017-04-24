using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlynnSQLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Inner Join");
            using (FlynnPracticalTestEntities context = new FlynnPracticalTestEntities())
            {
                try
                {
                    var query = from t1 in context.Table_1
                                join t2 in context.Table_2 on t1.Id equals t2.Id
                                select new { id = t1.Id, code = t1.Code, name = t2.Name };
                    foreach (var value in query)
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}"
                            ,value.id, value.code, value.name);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Left Join");
            using (FlynnPracticalTestEntities context = new FlynnPracticalTestEntities())
            {
                try
                {
                    var query = from t1 in context.Table_1
                                join t2 in context.Table_2 on t1.Id equals t2.Id into a
                                from b in a.DefaultIfEmpty()
                                select new { id = t1.Id, code = t1.Code, name = b.Name };
                    foreach (var value in query)
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}"
                            , value.id, value.code, value.name);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Right Join");
            using (FlynnPracticalTestEntities context = new FlynnPracticalTestEntities())
            {
                try
                {
                    var query = from t2 in context.Table_2
                                join t1 in context.Table_1 on t2.Id equals t1.Id into a
                                from b in a.DefaultIfEmpty()
                                select new { id = t2.Id, name = t2.Name, code = b.Code };
                    foreach (var value in query)
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}"
                            , value.id, value.code, value.name);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Full Outer Join");
            using (FlynnPracticalTestEntities context = new FlynnPracticalTestEntities())
            {
                try
                {
                    var query1 = from t1 in context.Table_1
                                join t2 in context.Table_2 on t1.Id equals t2.Id into gj
                                from subt2 in gj.DefaultIfEmpty()
                                select new { id = t1.Id, code = t1.Code, name = (subt2 == null ? String.Empty : subt2.Name) };
                    var query2 = from t2 in context.Table_2
                                join t1 in context.Table_1 on t2.Id equals t1.Id into gj
                                from subt1 in gj.DefaultIfEmpty()
                                select new { id = t2.Id, name = t2.Name, code = (subt1 == null ? String.Empty : subt1.Code) };
     
                    foreach (var value in query1)
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}"
                            , value.id, value.code, value.name);
                    }
                    foreach (var value in query2)
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}"
                            , value.id, value.code, value.name);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("Press return key to quit");
            Console.ReadLine();
            
        }
    }
}
