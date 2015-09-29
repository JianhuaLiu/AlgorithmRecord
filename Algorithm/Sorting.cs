//--------------------------------------------
// Copyright (C) 私人
// filename :Sorting
// created by 陈星宇
// at 2015/09/24 10:34:01
//--------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Algorithm
{
    /// <summary>
    /// 排序算法整理
    /// </summary>
    public class Sorting
    {
        #region 定时器
        //定义计时器
        private static Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// 开始计时
        /// </summary>
        public static void Go()
        {
            stopwatch.Reset();
            stopwatch.Start();
        }

        /// <summary>
        /// 停止计时
        /// </summary>
        /// <returns></returns>
        public static long Stop()
        {
            stopwatch.Stop();
            return stopwatch.ElapsedTicks;
        }
        #endregion

        /// <summary>
        /// 加载排序数列
        /// </summary>
        /// <returns></returns>
        public static IList<int> Loading(int max)
        {
            IList<int> lis = new List<int>();
            Random r = new Random();
            for (int i = 0; i < max; i++)
            {
                lis.Add(r.Next(1, 1000000));
            }
            return lis;
        }

        #region 冒泡排序
        /// <summary>
        /// 冒泡排序也被称为下沉排序，是一个简单的排序算法，通过多次重复比较每对相邻的元素，并按规定的顺序交换他们，最终把数列进行排好序。一直重复下去，直到结束。该算法得名于较小元素“气泡”会“浮到”列表顶部。由于只使用了比较操作，所以这是一个比较排序。
        ///  算法理解：嵌套循环，正反顺序比较相邻数据。并调整正确位置。
        /// </summary>
        /// <param name="list">杂乱无章的序列</param>
        /// <returns>整齐的序列</returns>
        public static IList<int> BubbleSort(IList<int> list)
        {
            try
            {
                //获取集合数量，比较排序，所以取倒数第二个
                int n = list.Count - 1;
                //大方向从前往后，一直到倒数第二个
                for (int i = 0; i < n; i++)
                {
                    //小方向从后往前，一直到大方向的索引
                    for (int j = n; j > i; j--)
                    {
                        //强转比较类型，从最后往前比较一位
                        if (((IComparable)list[j - 1]).CompareTo(list[j]) > 0)
                        {
                            //利用优先级
                            list[j - 1] = list[j] + (list[j] = list[j - 1]) * 0;

                            ////利用异或
                            //list[j - 1] ^= list[j];
                            //list[j] ^= list[j - 1];
                            //list[j - 1] ^= list[j];
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 双向冒泡排序
        /// <summary>
        /// 双向冒泡排序是一种稳定的比较排序算法。该算法不同于冒泡排序，它在排序上由两个方向同时进行。该排序算法只是比冒泡排序稍难实现，但解决了冒泡排序中的“乌龟问题”（数组尾部的小值）
        /// 算法理解：嵌套循环，中包含从左至右 与 从右至左，两种循环方式。比较相邻数据，并调整正确位置
        /// </summary>
        /// <param name="list">杂乱无章的序列</param>
        /// <returns>整齐的序列</returns>
        public static IList<int> BiDerectionalBubleSort(IList<int> list)
        {
            try
            {
                //获取集合数量
                int limite = list.Count;
                int st = -1;
                bool swapped = false;
                do
                {
                    swapped = false;
                    st++;
                    limite--;
                    //从左开始往右循环
                    for (int j = st; j < limite; j++)
                    {
                        //强转排序类型比较，如果左边比右边大
                        if (((IComparable)list[j]).CompareTo(list[j + 1]) > 0)
                        {
                            list[j] = list[j + 1] + (list[j + 1] = list[j]) * 0;
                            swapped = true;
                        }
                    }
                    //从右开始往左循环
                    for (int j = limite - 1; j >= st; j--)
                    {
                        //强转排序类型比较，如果左边比右边大
                        if (((IComparable)list[j]).CompareTo(list[j + 1]) > 0)
                        {
                            list[j] = list[j + 1] + (list[j + 1] = list[j]) * 0;
                            swapped = true;
                        }
                    }
                } while (st < limite && swapped);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 桶排序
        /// <summary>
        /// 桶排序是一种把数列划分成若干个桶的排序算法。在每个桶内各自排序，或者使用不同的排序算法，或通过递归方式继续使用桶排序算法。这是一个分布排序，是最能体现出数字意义的一种基数排序。桶排序是鸽巢排序的一种归纳结果。当要被排序的数组内的数值是均匀分配的时候，桶排序使用线性时间（Θ（n））。但桶排序并不是 比较排序，他不受到 O(n log n) 下限的影响。
        /// 算法理解：
        /// </summary>
        /// <param name="list">杂乱无章的序列</param>
        /// <returns>整齐的序列</returns>
        public static IList<int> BucketSort(IList<int> list)
        {
            int max = list[0];
            int min = list[0];
            //找集合中，最小值与最大值
            for (int i = 0; i < list.Count; i++)
            {
                if (((IComparable)list[i]).CompareTo(max) > 0)
                {
                    max = list[i];
                }

                if (((IComparable)list[i]).CompareTo(min) < 0)
                {
                    min = list[i];
                }
            }
            //定义一个足够大的容器。因为是最大值-最小值。所以肯定是足够装下所有集合。
            //注意事项：数组数量溢出
            ArrayList[] holder = new ArrayList[max - min + 1];
            //让数组变成二维数组
            for (int i = 0; i < holder.Length; i++)
            {
                holder[i] = new ArrayList();
            }
            //把集合的数据，付给二维数组
            for (int i = 0; i < list.Count; i++)
            {
                holder[list[i] - min].Add(list[i]);
            }
            int k = 0;
            //循环容器
            for (int i = 0; i < holder.Length; i++)
            {
                //判断是否有值
                if (holder[i].Count > 0)
                {
                    //重新给list进行赋值操作
                    for (int j = 0; j < holder[i].Count; j++)
                    {
                        list[k] = (int)holder[i][j];
                        k++;
                    }
                }
            }

            return list;
        }
        #endregion

        #region 梳排序
        /// <summary>
        /// 梳排序中，开始时的间距设定为列表长度，然后每一次都会除以损耗因子(一般为1.3)。必要的时候，间距可以四舍五入，不断重复，直到间距变为1。然后间距就保持为1，并排完整个数列。最后阶段就相当于一个冒泡排序，但此时大多数乌龟已经处理掉，此时的冒泡排序就高效了。
        /// </summary>
        /// <param name="list">杂乱无章的序列</param>
        /// <returns>整齐的序列</returns>
        public static IList<int> CombSort(IList<int> list)
        {
            //获取集合数量
            int gap = list.Count;
            int swaps = 0;

            do
            {
                //计算递减率，必须大于1
                gap = (int)(gap / 1.3);
                if (gap < 1)
                {
                    gap = 1;
                }
                int i = 0;
                swaps = 0;
                do
                {
                    //每次循环1与另一个数进行调换，直到循环尾部为止
                    if (((IComparable)list[i]).CompareTo(list[i + gap]) > 0)
                    {
                        list[i] = list[i + gap] + (list[i + gap] = list[i]) * 0;
                        swaps = 1;
                    }
                    i++;
                } while (!(i + gap >= list.Count));
            } while (!(gap == 1 && swaps == 0));
            return list;
        }
        #endregion

        #region 圈排序
        /// <summary>
        /// 它是一个就地、不稳定的排序算法，根据原始的数组，一种理论上最优的比较，并且与其它就地排序算法不同。它的思想是把要排的数列分解为圈，即可以分别旋转得到排序结果.
        /// 不同于其它排序的是，元素不会被放入数组的中任意位置从而推动排序。每个值如果它已经在其正确的位置则不动，否则只需要写一次即可。也就是说仅仅最小覆盖就能完成排序。
        /// </summary>
        /// <param name="list">杂乱无章的序列</param>
        /// <returns>整齐的序列</returns>
        public static IList<int> CycleSort(IList<int> list)
        {
            //循环每一个数组
            for (int cycleStart = 0; cycleStart < list.Count; cycleStart++)
            {
                int item = list[cycleStart];
                int pos = cycleStart;
                do
                {
                    int to = 0;
                    //循环整个数组，找到其相应的位置
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (i != cycleStart && ((IComparable)list[i]).CompareTo(item) < 0)
                        {
                            to++;
                        }
                    }
                    if (pos != to)
                    {
                        while (pos != to && ((IComparable)item).CompareTo(list[to]) == 0)
                        {
                            to++;
                        }
                        int temp = list[to];
                        list[to] = item;
                        item = temp;
                        pos = to;
                    }
                } while (cycleStart != pos);
            }
            return list;
        }
        #endregion

        #region 堆排序
        /// <summary>
        /// 堆排序是从数据集构建一个数据堆，然后提取最大元素，把它放到部分有序的数组的末尾。提取最大元素后，重新构建新堆，然后又接着提取新堆这的最大元素。重复这个过程，直到堆中没有元素并且数组已排好。堆排序的基本实现需要两个数组：一个用于构建堆，另一个是存放有序的元素。
        /// </summary>
        /// <param name="list">杂乱无章的序列</param>
        /// <returns>整齐的序列</returns>
        public static IList<int> HeapSort(IList<int> list)
        {
            //循环因为每次都能取出最大和最小，所以循环次数折中
            for (int i = (list.Count - 1) / 2; i >= 0; i--)
            {
                Adjust(list, i, list.Count - 1);
            }
            for (int i = list.Count - 1; i >= 1; i--)
            {
                list[i] = list[0] + (list[0] = list[i]) * 0;
                Adjust(list, 0, i - 1);
            }
            return list;
        }
        /// <summary>
        /// 推排序替换方法
        /// </summary>
        /// <param name="list">杂乱无章的序列</param>
        /// <param name="i">标识</param>
        /// <param name="m">总数</param>
        public static void Adjust(IList<int> list, int i, int m)
        {
            int temp = list[i];//获取该标识值
            int j = i * 2 + 1;//获取对应尾部标识
            while (j <= m) //循环直到标识 <= 总数
            {
                if (j < m) //尾部标识 小于 总数
                {
                    //如果左边小于右边，右边标识加一位
                    if (((IComparable)list[j]).CompareTo(list[j + 1]) < 0)
                    {
                        j = j + 1;
                    }
                }
                if (((IComparable)temp).CompareTo(list[j]) < 0)
                {
                    //交换位置
                    list[i] = list[j];
                    i = j;
                    j = 2 * i + 1;
                }
                else
                {
                    //结束循环
                    j = m + 1;
                }
            }
            list[i] = temp;
        }
        #endregion

        #region 插入排序
        /// <summary>
        /// 插入排序的工作原理是通过构建有序序列，对于未排序数据，在已排序序列中从后向前扫描，找到相应位置并插入。插入排序在实现上，通常采用in-place排序（即只需用到O(1)的额外空间的排序），因而在从后向前扫描过程中，需要反复把已排序元素逐步向后挪位，为最新元素提供插入空间。
        /// </summary>
        /// <param name="list">杂乱无章的序列</param>
        /// <returns>整齐的序列</returns>
        public static IList<int> InsertionSort(IList<int> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                int val = list[i];
                int j = i - 1;
                bool done = false;
                do
                {
                    if (((IComparable)list[j]).CompareTo(val) > 0)
                    {
                        list[j + 1] = list[j];
                        j--;
                        if (j < 0)
                        {
                            done = true;
                        }
                    }
                    else
                    {
                        done = true;
                    }
                } while (!done);
                list[j + 1] = val;
            }
            return list;
        }
        #endregion

        #region 奇偶排序
        /// <summary>
        /// 奇偶排序通过比较所有相邻的(奇数偶)对进行排序，如果某对存在错误的顺序(第一个元素大于第二个)，则交换。下一步针对｛偶奇对｝重复这一操作。然后序列就在(奇，偶)和(偶，奇)之间变换，直到列表有序。它可以看作是是使用并行处理器，每个都用了冒泡排序，但只是起始点在列表的不同位置(所有奇数位置可做第一步)
        /// </summary>
        /// <param name="list">杂乱无章的序列</param>
        /// <returns>整齐的序列</returns>
        public static IList<int> OddEventSort(IList<int> list)
        {
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (int i = 1; i < list.Count - 1; i += 2)
                {
                    if (((IComparable)list[i]).CompareTo(list[i + 1]) > 0)
                    {
                        list[i] = list[i + 1] + (list[i + 1] = list[i]) * 0;
                        sorted = false;
                    }
                }
                for (int i = 0; i < list.Count - 1; i += 2)
                {
                    if (((IComparable)list[i]).CompareTo(list[i + 1]) > 0)
                    {
                        list[i] = list[i + 1] + (list[i + 1] = list[i]) * 0;
                        sorted = false;
                    }

                }
            }
            return list;
        }
        #endregion

        #region 鸽巢排序
        /// <summary>
        /// 鸽巢排序假设有个一个待排序的数组，给它建立了一个空的辅助数组（称为“鸽巢”）。把原始数组中的每个值作为一个key(“格子”)。遍历原始数组，根据每个值放入辅助数组对应的“格子”。顺序遍历“鸽巢”数组（辅助数组），把非空鸽巢中的元素放回原始数组。（在不可避免遍历每一个元素并且排序的情况下效率最好的一种排序算法。但它只有在差值(或者可被映射在差值)很小的范围内的数值排序的情况下实用。）
        /// </summary>
        /// <param name="list">杂乱无章的序列</param>
        /// <returns>整齐的序列</returns>
        public static IList<int> PigeonHoleSort(IList<int> list)
        {
            int min = list[0], max = list[0];
            foreach (int x in list)
            {
                if (((IComparable)min).CompareTo(x) > 0)
                {
                    min = x;
                }
                if (((IComparable)max).CompareTo(x) < 0)
                {
                    max = x;
                }
            }
            int size = max - min + 1;
            int[] holes = new int[size];
            foreach (int x in list)
            {
                holes[x - min]++;
            }
            int i = 0;
            for (int count = 0; count < size; count++)
            {
                while (holes[count]-- > 0)
                {
                    list[i] = count + (int)min;
                    i++;
                }
            }
            return list;
        }
        #endregion

        #region 快速排序
        /// <summary>
        /// 快速排序采用分而治之的策略，把一个列表划分为两个子列表。步骤是：
        /// 从列表中，选择一个元素，称为基准（pivot）。
        /// 重新排序列表，把所有数值小于枢轴的元素排到基准之前，所有数值大于基准的排基准之后(相等的值可以有较多的选择)。在这/   个分区退出之后，该基准就处于数列的中间位置。这个称为分区（partition）操作。
        /// 分别递归排序较大元素的子列表和较小的元素的子列表。
        /// </summary>
        /// <param name="list">杂乱无章的序列</param>
        /// <returns>整齐的序列</returns>
        public static IList<int> QuickSort(IList<int> list, int left, int right)
        {
            right = right == 0 ? list.Count - 1 : right;
            int i = left, j = right;
            double privotValue = (left + right) / 2;
            int x = list[(int)privotValue];
            while (i <= j)
            {
                while (((IComparable)list[i]).CompareTo(x) < 0)
                {
                    i++;
                }
                while (((IComparable)x).CompareTo(list[j]) < 0)
                {
                    j--;
                }
                if (i <= j)
                {
                    list[i] = list[j] + (list[j] = list[i]) * 0;
                    i++;
                    j--;
                }
            }
            if (left < j)
            {
                QuickSort(list, left, j);
            }
            if (i < right)
            {
                QuickSort(list, i, right);
            }
            return list;
        }
        #endregion

        #region 选择排序
        /// <summary>
        /// 首先在未排序序列中找到最小（大）元素，存放到排序序列的起始位置，然后，再从剩余未排序元素中继续寻找最小（大）元素，然后放到已排序序列的末尾。以此类推，直到所有元素均排序完毕。
        /// </summary>
        /// <param name="list">杂乱无章的序列</param>
        /// <returns>整齐的序列</returns>
        public static IList<int> SelectionSort(IList<int> list)
        {
            int min;
            for (int i = 0; i < list.Count; i++)
            {
                min = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (((IComparable)list[j]).CompareTo(list[min]) < 0)
                    {
                        min = j;
                    }
                }
                list[i] = list[min] + (list[min] = list[i]) * 0;
            }
            return list;
        }
        #endregion

        #region 希尔排序
        /// <summary>
        /// 希尔排序通过将比较的全部元素分为几个区域来提升插入排序的性能。这样可以让一个元素可以一次性地朝最终位置前进一大步。然后算法再取越来越小的步长进行排序，算法的最后一步就是普通的插入排序，但是到了这步，需排序的数据几乎是已排好的了（此时插入排序较快）。
        /// </summary>
        /// <param name="list">杂乱无章的序列</param>
        /// <returns>整齐的序列</returns>
        public static IList<int> ShellSort(IList<int> list)
        {
            int i, j, increment;
            int temp;
            increment = list.Count / 2;
            while (increment>0)
            {
                for (i = 0; i < list.Count; i++)
                {
                    j = i;
                    temp = list[i];
                    while ((j>=increment) && ((IComparable)list[j-increment]).CompareTo(temp) >0)
                    {
                        list[j] = list[j - increment];
                        j = j - increment;
                    }
                    list[i] = temp;
                }
                if (increment == 2)
                {
                    increment = 1;
                }
                else
                {
                    increment = increment * 5 / 11;
                }
            }
            return list;
        }
        #endregion
    }
}
