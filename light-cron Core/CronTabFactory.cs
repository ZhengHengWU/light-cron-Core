using Light.Cron;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Md.Api.Service.Eq
{
    [CrontabJob]
    public class CronTabFactory
    {
        // contab 表达式解析
        // 这里的* * * * * 分别代表分　时　日　月　周
        // 第1列表示分钟0～59 第2列表示小时0～23 第3列表示日期1～31 新增e标记作为月末最后一天
        // 第4列表示月份1～12 第5列标识号星期0～7（0和7表示星期天）
        // 每一个*可以直接写具体时间
        // 举几个栗子 
        // * 0-11 * * *|*/5 12-23 * * * 就是0点至11点, 每分钟执行一次, 12点至23点, 每5分钟执行一次
        // */1 * * * * 就是每一分钟执行一次
        // 0 5 1,15,e * * 每月1日，15日和最后1日的早上5点
        // 0 */2 * * * 每隔两个小时 
        // 30 1 */1 * * 每天凌晨1:30
        // */1 * * * *  每隔一分钟
        // 0 8 * * *  每天八点
        Task task = null;
        // <summary>
        // 每天早上八点执行预警推送
        // 过期时间默认60天
        // </summary>
        [CrontabSchedule("warningPush", "*/1 * * * *")]
        public void WarringPush()
        {
            string fileName = @"D:\Log\Light.Cron.txt";
            StreamWriter writer = new StreamWriter(fileName, true);
            task = writer.WriteLineAsync(string.Format("{0},预警推送!", DateTime.Now.ToLongTimeString()));
            writer.Close();
            writer.Dispose();
        }
    }
}
