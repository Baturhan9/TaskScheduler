using Models;

namespace Contracts;

public interface IReportMaker
{
    void MakeReports(List<Tasks> tasks, string pathToSave);
}