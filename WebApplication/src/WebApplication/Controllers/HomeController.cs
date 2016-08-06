using System;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var con = new MySqlConnection("server=localhost;database=world;uid=root;pwd=123456;charset='gbk'");
            //新增数据
            con.Execute("insert into city values(null, 'Dalian', 'CHN', 'Insert', '1800000')");
            //新增数据返回自增id
            var id =
                con.QueryFirst<int>(
                    "insert into city values(null, 'Beijing', 'CHN', 'Insert', '7800000'); select last_insert_id();");
            //修改数据
            con.Execute("update city set Name = 'Beijing' where Id = @Id", new {Id = id});
            //查询数据
            var list = con.Query<City>("select * from city");
            foreach (var item in list)
            {
                Console.WriteLine($"城市名称：{item.Name} 人口：{item.Population}");
            }
            //删除数据
            con.Execute("delete from city where Id = @Id", new {Id = id});
            Console.WriteLine("删除数据后的结果");
            list = con.Query<City>("select * from city");
            foreach (var item in list)
            {
                Console.WriteLine($"城市名称：{item.Name} 人口：{item.Population}");
            }
            Console.ReadKey();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}