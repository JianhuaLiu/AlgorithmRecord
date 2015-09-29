using Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AlgorithmConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化一遍，测试是否正常
            try
            {
                Sorting.Go();
                Sorting.Stop();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TestSpeed("冒泡", "BubbleSort", false);
            TestSpeed("双向冒泡", "BiDerectionalBubleSort", false);
            TestSpeed("桶", "BucketSort", true, true);
            TestSpeed("梳", "CombSort", true, true);
            TestSpeed("圈", "CycleSort", false);
            TestSpeed("堆", "HeapSort", true, true);
            TestSpeed("插入", "InsertionSort", false);
            TestSpeed("奇偶", "OddEventSort", false);
            TestSpeed("鸽巢", "PigeonHoleSort", true, true);
            TestSpeed("快速", "QuickSort", true, true);
            TestSpeed("选择", "SelectionSort", false);
            TestSpeed("希尔", "ShellSort", true, false);

            var objYq = yq.OrderBy(x => x.Value).First();
            var objYw = yw.OrderBy(x => x.Value).First();
            var objSw = sw.OrderBy(x => x.Value).First();
            var objBw = bw.OrderBy(x => x.Value).First();

            Console.WriteLine(" \n 测试结果");
            Console.WriteLine("一千条数据，“" + objYq.Key + "”速度最快。用时：" + objYq.Value + "；约等于：" + objYq.Value / 1000 + "毫秒；" + objYq.Value / 1000000 + "秒");
            Console.WriteLine("一万条数据，“" + objYw.Key + "”速度最快。用时：" + objYw.Value + "；约等于：" + objYw.Value / 1000 + "毫秒；" + objYw.Value / 1000000 + "秒");
            Console.WriteLine("五万条数据，“" + objSw.Key + "”速度最快。用时：" + objSw.Value + "；约等于：" + objSw.Value / 1000 + "毫秒；" + objSw.Value / 1000000 + "秒");
            Console.WriteLine("百万条数据，“" + objBw.Key + "”速度最快。用时：" + objBw.Value + "；约等于：" + objBw.Value / 1000 + "毫秒；" + objBw.Value / 1000000 + "秒");
            Console.ReadLine();
        }

        #region 结果字典
        // 一千数据结果集
        private static Dictionary<string, long> yq = new Dictionary<string, long>();
        // 一万数据结果集
        private static Dictionary<string, long> yw = new Dictionary<string, long>();
        // 十万数据结果集
        private static Dictionary<string, long> sw = new Dictionary<string, long>();
        // 百万数据结果集
        private static Dictionary<string, long> bw = new Dictionary<string, long>();
        #endregion

        /// <summary>
        /// 测试速度方法
        /// </summary>
        /// <param name="name">排序名称</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="isWw">是否继续五万数据测试</param>
        public static void TestSpeed(string name, string methodName, bool isWw = true, bool isBw = false)
        {
            Type type = typeof(Sorting);
            object refle = Activator.CreateInstance(type);
            MethodInfo info = type.GetMethod(methodName);
            Console.WriteLine(" \n 开始测试" + name + "排序速度");
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            IList<int> list = Sorting.Loading(1000);
                            Sorting.Go();
                            object[] obj = new object[] { list };
                            if (name == "快速")
                            {
                                obj = new object[] { list, 0, list.Count - 1 };
                            }
                            info.Invoke(refle, obj);
                            long org = Sorting.Stop();
                            Console.WriteLine("一千数据，“" + name + "”排序用时：" + org + "微秒；约等于：" + org / 1000 + "毫秒；" + org / 1000000 + "秒");
                            yq.Add(name, org);
                            break;
                        }
                    case 1:
                        {
                            IList<int> list = Sorting.Loading(10000);
                            Sorting.Go();
                            object[] obj = new object[] { list };
                            if (name == "快速")
                            {
                                obj = new object[] { list, 0, list.Count - 1 };
                            }
                            info.Invoke(refle, obj);
                            long org = Sorting.Stop();
                            Console.WriteLine("一万数据，“" + name + "”排序用时：" + org + "微秒；约等于：" + org / 1000 + "毫秒；" + org / 1000000 + "秒");
                            yw.Add(name, org);
                            break;
                        }
                    case 2:
                        {
                            if (isWw)
                            {
                                IList<int> list = Sorting.Loading(50000);
                                Sorting.Go();
                                object[] obj = new object[] { list };
                                if (name == "快速")
                                {
                                    obj = new object[] { list, 0, list.Count - 1 };
                                }
                                info.Invoke(refle, obj);
                                long org = Sorting.Stop();
                                Console.WriteLine("五万数据，“" + name + "”排序用时：" + org + "微秒；约等于：" + org / 1000 + "毫秒；" + org / 1000000 + "秒");
                                sw.Add(name, org);
                            }
                            break;
                        }
                    case 3:
                        {
                            if (isBw)
                            {
                                IList<int> list = Sorting.Loading(1000000);
                                Sorting.Go();
                                object[] obj = new object[] { list };
                                if (name == "快速")
                                {
                                    obj = new object[] { list, 0, list.Count - 1 };
                                }
                                info.Invoke(refle, obj);
                                long org = Sorting.Stop();
                                Console.WriteLine("一百万数据，“" + name + "”排序用时：" + org + "微秒；约等于：" + org / 1000 + "毫秒；" + org / 1000000 + "秒");
                                bw.Add(name, org);
                            }
                            break;
                        }
                    default:
                        break;
                }

            }
            Console.WriteLine();
        }
    }
}
