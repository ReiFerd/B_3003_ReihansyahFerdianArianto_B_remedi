using System;
using System.Collections.Generic;
using System.Text;

namespace PBORemed
{
    class Agen
    {
        public string idAgen { get; set; }
        public string namaAgen { get; set; }
        public int stokAgen { get; set; }

        public void TampilInfo()
        {
            Console.WriteLine("ID Agen: " + idAgen + ", Nama Agen: " + namaAgen + ", Stok Agen: " + stokAgen);
        }
    }

    abstract class ProdukGalon
    {
        private string idProduk;
        private string namaProduk;
        private int stokGudang;

        public string IdProduk
        {
            get { return idProduk; }
            set { idProduk = value; }
        }

        public string NamaProduk
        {
            get { return namaProduk; }
            set { namaProduk = value; }
        }

        public int StokGudang
        {
            get { return stokGudang; }
            set
            {
                if (value >= 0)
                {
                    stokGudang = value;
                }
            }
        }

        public void KurangiStok(int jumlah)
        {
            if (jumlah > 0 && jumlah <= stokGudang)
            {
                stokGudang -= jumlah;
            }
        }

        public abstract int HitungBiaya(int jumlah);
    }

    class GalonAkua : ProdukGalon
    {
        public override int HitungBiaya(int jumlah)
        {
            return jumlah * 20000;
        }
    }

    class GalonBerkeran : ProdukGalon
    {
        public override int HitungBiaya(int jumlah)
        {
            return jumlah * 12000;
        }
    }

    interface DapatDidistribusikan
    {
        void KirimPaket();
    }

    class DistribusiGalon : DapatDidistribusikan
    {
        public Agen agenTujuan { get; set; }
        public ProdukGalon produkKirim { get; set; }
        public int jumlahKirim { get; set; }

        public DistribusiGalon(Agen agen, ProdukGalon produk, int jumlah)
        {
            agenTujuan = agen;
            produkKirim = produk;
            jumlahKirim = jumlah;
        }

        public void KirimPaket()
        {
            if (jumlahKirim > 0 && jumlahKirim <= produkKirim.StokGudang)
            {
                produkKirim.KurangiStok(jumlahKirim);
                agenTujuan.stokAgen += jumlahKirim;
                Console.WriteLine("Paket berhasil dikirimkan ke agen " + agenTujuan.namaAgen + "...");
            }
            else
            {
                Console.WriteLine("Pengiriman gagal! Jumlah kirim tidak valid atau melebihi stok gudang.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Agen agen1 = new Agen();
            agen1.idAgen = "A1";
            agen1.namaAgen = "Lola";
            agen1.stokAgen = 500;

            Agen agen2 = new Agen();
            agen2.idAgen = "A2";
            agen2.namaAgen = "Lala";
            agen2.stokAgen = 200;

            agen1.TampilInfo();
            agen2.TampilInfo();

            ProdukGalon produk1 = new GalonAkua();
            produk1.IdProduk = "P1";
            produk1.NamaProduk = "Galon Akua";
            produk1.StokGudang = 100;

            ProdukGalon produk2 = new GalonBerkeran();
            produk2.IdProduk = "P2";
            produk2.NamaProduk = "Galon Berkeran";
            produk2.StokGudang = 150;

            Console.WriteLine(produk1.NamaProduk + " Biaya 10 galon: " + produk1.HitungBiaya(10));
            Console.WriteLine(produk2.NamaProduk + " Biaya 2 galon: " + produk2.HitungBiaya(2));

            DistribusiGalon distribusi = new DistribusiGalon(agen1, produk1, 10);
            distribusi.KirimPaket();
        }
    }
}
