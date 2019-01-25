using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAllDistr
{
    public struct Data1
    {
        public string name;
        public LinkedList<Data1> list;
    }

    class Program
    {
         static object _locker = new object();

         static void Main(string[] args)
        {
            string dirName = DirName();
            LinkedList<Data1> Dir = new LinkedList<Data1>();
            Task task = Task.Factory.StartNew(() => GetAllDir(dirName, Dir));

            Console.ReadLine();
            CoutLinkedList(Dir);
            Console.ReadLine();
        }

        static string DirName()
        {

            Console.WriteLine("Please enter start dictionary");
            string str = Console.ReadLine();
            while (!Directory.Exists(str))
            {
                Console.WriteLine("Please try again!!!");
                str = Console.ReadLine();
            }

            return str;
        }

        static void GetAllDir(string dirName, LinkedList<Data1> dir)
        {

            string[] dirs = Directory.GetDirectories(dirName);
            string[] files = Directory.GetFiles(dirName);

            lock (_locker)
            {
                foreach (var s in dirs)
                {
                    Data1 q;

                    q.name = s;
                    q.list = new LinkedList<Data1>();
                    dir.Add(q);
                    Task task = Task.Factory.StartNew(() => GetAllDir(s, q.list));
                }

                foreach (string s in files)
                {
                    Data1 q;

                    q.name = s;
                    q.list = null;
                    dir.Add(q);
                }
            }

        }

        static void CoutLinkedList(LinkedList<Data1> list)
        {
            foreach (Data1 q in list)
            {
                Console.WriteLine(q.name);
                if(q.list !=null)
                    CoutLinkedList(q.list);
            }
        }
    }
}
