using System.Runtime.CompilerServices;

namespace MetricsAgent.Extentions
{
    //методы расширения можно создавать только в статическом классе
    public static class ListExtentions
    {
        //this в параметрах указывает на наш объект
        //методы помечанне стрелокой - обозначают расширение
        public static List<int> GetEvenNumbers(this List<int> list)
        {
            return list.FindAll(x => x % 2 == 0);
        }
    }
}
