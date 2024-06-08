using System.Text;
using Contracts;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using OpenXMLTemplates.Documents;

namespace Services;

public class ReportMaker : IReportMaker
{
    public void MakeReports(List<Models.Tasks> tasks, string pathToSave)
    {
        CreateFile(pathToSave);
        File.Copy(@"..\..\docs\template.docx", pathToSave, true);
        using (WordprocessingDocument document = WordprocessingDocument.Open(pathToSave, true))
        {
            // If your sourceFile is a different type (e.g., .DOTX), you will need to change the target type like so:
            document.ChangeDocumentType(WordprocessingDocumentType.Document);

            // Get the MainPart of the document
            MainDocumentPart mainPart = document.MainDocumentPart;
            List<FieldCode> mergeFields = new();
            foreach(var header in mainPart.RootElement)
                    mergeFields.AddRange(header.Descendants<FieldCode>());


            StringBuilder sb = new StringBuilder();
            foreach (var task in tasks)
            {
                string admin = "----";
                string completedDate = task.CompletedDate.ToString() ?? "----";
                if (task.TechAdmin is not null)
                    admin = task.TechAdmin.FirstName + " " + task.TechAdmin.LastName;
                sb.Append($"Задача №{task.Id}:\n\tВыполнил/Выполняет: {admin}\n\tДата завершения: {completedDate}\n\n");
            }
            string[] replacementTexts = new[]{
                tasks.Count().ToString(),
                tasks.Count(x => x.AcceptedUserAdminId == null).ToString(),
                tasks.Count(x => x.AcceptedUserAdminId is not null && x.CompletedDate is null).ToString(),
                tasks.Count(x => x.isCompleted && x.CompletedDate is not null).ToString(),
                sb.ToString()
            };
            string[] mergeFieldNames = new[] {"TotalTasks", "IssuedTasks", "DoingTasks", "DoneTasks", "FullInfoAboutTasks"};
            for(int i = 0; i < replacementTexts.Length; i++)
                ReplaceMergeFieldWithText(mergeFields, mergeFieldNames[i], replacementTexts[i]);                   

            // Save the document
            mainPart.Document.Save();
        }
    }


    private void ReplaceMergeFieldWithText(IEnumerable<FieldCode> fields, string mergeFieldName, string replacementText)
    {
        var field = fields
            .Where(f => f.InnerText.Contains(mergeFieldName))
            .FirstOrDefault();

        if (field != null)
        {
            // Get the Run that contains our FieldCode
            // Then get the parent container of this Run
            Run rFldCode = (Run)field.Parent; 

            // Get the three (3) other Runs that make up our merge field
            Run rBegin = rFldCode.PreviousSibling<Run>();
            Run rSep = rFldCode.NextSibling<Run>();
            Run rText = rSep.NextSibling<Run>();
            Run rEnd = rText.NextSibling<Run>();

            // Get the Run that holds the Text element for our merge field
            // Get the Text element and replace the text content 
            Text t = rText.GetFirstChild<Text>();
            t.Text = replacementText;

            // Remove all the four (4) Runs for our merge field
            rFldCode.Remove();
            rBegin.Remove();
            rSep.Remove();
            rEnd.Remove();
        }

    }


    private void CreateFile(string pathToSave)
    {
        using (FileStream fs = File.Create(pathToSave))
        {
            // Создать Word-документ
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(fs, WordprocessingDocumentType.Document))
            {
                // Создать основную часть документа
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                // Создать тело документа
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                // Добавить текст в тело документа
                Paragraph paragraph = body.AppendChild(new Paragraph());
                Run run = paragraph.AppendChild(new Run());
                Text text = run.AppendChild(new Text("Example text."));

                // Сохранить изменения
                wordDocument.Save();
            }
        }
    }
}