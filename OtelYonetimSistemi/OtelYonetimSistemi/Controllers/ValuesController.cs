using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using OtelYonetimSistemi.DbConnection;
using OtelYonetimSistemi.Entity;

namespace OtelYonetimSistemi.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;
        private IConfiguration _configuration;
        public ValuesController(IDbConnection dbConnection, IConfiguration configuration)
        {
            _dbConnection = dbConnection;
            _configuration = configuration;
        }
        [HttpGet("MusterileriGetir")]
        public IActionResult MusterileriGetir()
        {
            var musteri = new List<Musteriler>();
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbLocalConnection")))
            {
                musteri = db.Query<Musteriler>("Select * From musteriler").ToList();
            }
            return Ok(musteri);
        }

        [HttpGet("CalisanlariGetir")]
        public IActionResult CalisanlariGetir()
        {
            var calisan = new List<Calisanlar>();
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbLocalConnection")))
            {
                calisan = db.Query<Calisanlar>("Select * From calisanlar").ToList();
            }
            return Ok(calisan);
        }

        [HttpGet("OtelleriGetir")]
        public IActionResult OtelleriGetir()
        {
            var otel = new List<Oteller>();
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbLocalConnection")))
            {
                otel = db.Query<Oteller>("Select * From oteller").ToList();
            }
            return Ok(otel);
        }

        [HttpGet("RezervasyonlariGetir")]
        public IActionResult RezervasyonlariGetir()
        {
            var rezervasyon = new List<Rezervasyonlar>();
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbLocalConnection")))
            {
                rezervasyon = db.Query<Rezervasyonlar>("Select * From rezervasyonlar").ToList();
            }
            return Ok(rezervasyon);
        }

        [HttpPost("MusteriGir")]
        public IActionResult MusteriGir(Musteriler musteri)
        {
            using (IDbConnection db = new SqlConnection(new GetConnectionString().GetConnection))
            {
                string SqlQuery = @"INSERT INTO Musteriler (MusteriAdi, MusteriSoyAdi, Email) OUTPUT INSERTED.* VALUES(@MusteriAdi, @MusteriSoyAdi, @Email)";
                return Ok(db.Query(SqlQuery, musteri));
            }
        }

        [HttpPost("CalisanGir")]
        public IActionResult CalisanGir(Calisanlar calisan)
        {
            using (IDbConnection db = new SqlConnection(new GetConnectionString().GetConnection))
            {
                string SqlQuery = @"INSERT INTO Calisanlar (CalisanAdi, CalisanSoyAdi, Telefon, Pozisyon, OtelId) OUTPUT INSERTED.* VALUES(@CalisanAdi, @CalisanSoyAdi, @Telefon, @Pozisyon, @OtelId)";
                return Ok(db.Query(SqlQuery, calisan));
            }
        }

        [HttpPut("MusteriGuncelle")]
        public IActionResult MusteriGuncelle(Musteriler musteri)
        {
            using (IDbConnection db = new SqlConnection(new GetConnectionString().GetConnection))
            {
                string sqlQuery = @"UPDATE Musteriler SET  MusteriAdi=@MusteriAdi,MusteriSoyAdi=@MusteriSoyAdi,Email=@Email
                        OUTPUT INSERTED.* 
                        WHERE MusteriId=@MusteriId";

                return Ok(db.Query(sqlQuery, musteri));
            }
        }

        [HttpPut("CalisanGuncelle")]
        public IActionResult CalisanGuncelle(Calisanlar calisan)
        {
            using (IDbConnection db = new SqlConnection(new GetConnectionString().GetConnection))
            {
                string sqlQuery = @"UPDATE Calisanlar SET  CalisanAdi=@CalisanAdi,CalisanSoyAdi=@CalisanSoyAdi,Telefon=@Telefon,Pozisyon=@Pozisyon,OtelId=@OtelId
                        OUTPUT INSERTED.* 
                        WHERE CalisanId=@CalisanId";

                return Ok(db.Query(sqlQuery, calisan));
            }
        }

        [HttpDelete("MusteriSil")]
        public IActionResult MusteriSil(Musteriler musteri)
        {
            using (IDbConnection db = new SqlConnection(new GetConnectionString().GetConnection))
            {
                string sqlQuery = @"DELETE FROM Musteriler  WHERE MusteriId = @MusteriId";
                int rowsAffected = db.Execute(sqlQuery, musteri);
                return Ok(rowsAffected);
            }
        }

        [HttpDelete("CalisanSil")]
        public IActionResult CalisanSil(Calisanlar calisan)
        {
            using (IDbConnection db = new SqlConnection(new GetConnectionString().GetConnection))
            {
                string sqlQuery = @"DELETE FROM Calisanlar  WHERE CalisanId = @CalisanId";
                int rowsAffected = db.Execute(sqlQuery, calisan);
                return Ok(rowsAffected);
            }
        }

    }
}
