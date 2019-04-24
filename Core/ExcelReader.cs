using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectFinal101.Core
{
    public class ExcelReader
    {
        private readonly IHostingEnvironment _host;

        public ExcelReader(IHostingEnvironment host)
        {
            _host = host;
        }


        private async Task<string> SaveFile(IFormFile file)
        {
            var uploadPath = Path.Combine(_host.ContentRootPath, "uploads");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }

        public async Task<IList<ExcelResource>> GetValues(IFormFile file)
        {
            try
            {
                var path = await SaveFile(file);

                var workBook = new XLWorkbook(path);

                var excelResources = new List<ExcelResource>();

                foreach (var worksheet in workBook.Worksheets)
                {
                    var row = worksheet
                        .FirstRowUsed()
                        .RowUsed();

                    row = row.RowBelow();

                    while (!row.Cell(1).IsEmpty())
                    {
                        var col1 = row.Cell(2).GetString().Trim();
                        var col2 = row.Cell(3).GetString().Trim();

                        if (!string.IsNullOrEmpty(col1) && !string.IsNullOrEmpty(col2))
                        {
                            if (excelResources.All(x => x.ColumnOne != col1))
                            {
                                var resource = new ExcelResource
                                {
                                    ColumnTwo = col2,
                                    ColumnOne = col1
                                };

                                excelResources.Add(resource);
                            };
                        }

                        row = row.RowBelow();

                    }
                }

                return excelResources;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IList<Course>> GetCourses(IFormFile courses)
        {
            try
            {
                var path = await SaveFile(courses);

                var workBook = new XLWorkbook(path);

                var courseList = new List<Course>();

                foreach (var worksheet in workBook.Worksheets)
                {
                    var row = worksheet
                        .FirstRowUsed()
                        .RowUsed();

                    row = row.RowBelow();

                    while (!row.Cell(1).IsEmpty())
                    {
                        var courseTitle = row.Cell(2).GetString().Trim();
                        var courseCode = row.Cell(3).GetString().Trim();
                        int.TryParse(row.Cell(4).GetString().Trim(), out var courseCredit);

                        if (!string.IsNullOrEmpty(courseTitle) && !string.IsNullOrEmpty(courseCode) && courseCredit != 0)
                        {
                            if (courseList.All(x => x.CourseCode != courseCode))
                            {
                                var course = new Course
                                {
                                    CourseCode = courseCode,
                                    Credit = courseCredit,
                                    Title = courseTitle
                                };

                                courseList.Add(course);
                            };
                        }

                        row = row.RowBelow();

                    }
                }

                return courseList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
