using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTinLanh.Data
{
    // MediaTinLanhDB. hosts all repositories

    public static class MediaTinLanhDB
    {
        static MediaTinLanhContext db = new MediaTinLanhContext();

        // entity specific repositories

        public static BanDichCaus BanDichCaus { get { return new BanDichCaus(db); } }
        public static BanDichSachs BanDichSachs { get { return new BanDichSachs(db); } }
        public static BanDichPhienBans BanDichPhienBans { get { return new BanDichPhienBans(db); } }
        public static BoDes BoDes { get { return new BoDes(db); } }
        public static CauDos CauDos { get { return new CauDos(db); } }
        public static CauHois CauHois { get { return new CauHois(db); } }
        public static DapAns DapAns { get { return new DapAns(db); } }
        public static ChuDes ChuDes { get { return new ChuDes(db); } }
        public static CauKinhThanhs CauKinhThanhs { get { return new CauKinhThanhs(db); } }
        public static GopYPhanMems GopYPhanMems { get { return new GopYPhanMems(db); } }
        public static LoaiThanhCas LoaiThanhCas { get { return new LoaiThanhCas(db); } }
        public static ThanhCas ThanhCas { get { return new ThanhCas(db); } }
        public static MediaTypes MediaTypes { get { return new MediaTypes(db); } }
        public static Medias Medias { get { return new Medias(db); } }
        public static MediaThanhCas MediaThanhCas { get { return new MediaThanhCas(db); } }
        public static Templates Templates { get { return new Templates(db); } }
        public static NgonNgus NgonNgus { get { return new NgonNgus(db); } }
        public static PhienBans PhienBans { get { return new PhienBans(db); } }
        public static Sachs Sachs { get { return new Sachs(db); } }
        public static LoiBaiHats LoiBaiHats { get { return new LoiBaiHats(db); } }

        // general purpose operations

        //public static void Execute(string sql, params object[] parms) { db.Execute(sql, parms); }
        //public static IEnumerable<dynamic> Query(string sql, params object[] parms) { return db.Query(sql, parms); }
        //public static object Scalar(string sql, params object[] parms) { return db.Scalar(sql, parms); }

        //public static DataSet GetDataSet(string sql, params object[] parms) { return db.GetDataSet(sql, parms); }
        //public static DataTable GetDataTable(string sql, params object[] parms) { return db.GetDataTable(sql, parms); }
        //public static DataRow GetDataRow(string sql, params object[] parms) { return db.GetDataRow(sql, parms); }
    }
}
