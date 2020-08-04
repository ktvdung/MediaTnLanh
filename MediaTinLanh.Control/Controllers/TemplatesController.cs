using AutoMapper;
using MediaTinLanh.Data;
using MediaTinLanh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbMediaTinLanh = MediaTinLanh.Data.MediaTinLanhContext;

namespace MediaTinLanh.Control
{
    public class TemplatesController : BaseController
    {
        public static TemplatesController Context = new TemplatesController();

        public IEnumerable<TemplateModel> All()
        {
            var Templates = dbMediaTinLanh.Templates.All();
            return Mapper.Map<IEnumerable<Template>, IEnumerable<TemplateModel>>(Templates);
        }

        public IEnumerable<TemplateModel> Query(string filter, params object[] paramaters)
        {
            var Templates = dbMediaTinLanh.Templates.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<Template>, IEnumerable<TemplateModel>>(Templates);
        }

        public TemplateModel Single(int? Id)
        {
            var Template = dbMediaTinLanh.Templates.Single(Id);
            return Mapper.Map<Template, TemplateModel>(Template);
        }

        public int? Insert(TemplateModel TemplateModel)
        {
            var Template = Mapper.Map<TemplateModel, Template>(TemplateModel);
            return dbMediaTinLanh.Templates.Insert(Template);
        }

        public int? Update(int? Id, TemplateModel TemplateModel)
        {
            var TemplateExists = dbMediaTinLanh.Templates.Single(Id);
            if (TemplateExists != null)
            {
                TemplateModel.Id = TemplateExists.Id;
                dbMediaTinLanh.Templates.Update(Mapper.Map<TemplateModel, Template>(TemplateModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var TemplateExists = dbMediaTinLanh.Templates.Single(Id);
            if (TemplateExists != null)
            {
                dbMediaTinLanh.Templates.Delete(TemplateExists);
                return 1;
            }
            return 0;
        }
    }
}
