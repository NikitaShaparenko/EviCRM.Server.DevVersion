using EviCRM.Server.Core;
using EviCRM.Server.Models;
using FluentFTP;
using Meziantou.Framework;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;

namespace EviCRM.Server.Pages.Store
{
    public partial class FileStoreComponent
    {
        public static string sftp_url_server = "78.140.233.179";
        public static int sftp_url_port = 9021;
        public static string sftp_user = "portal";
        public static string sftp_password = "e5CbtQxX2FsC";

        #region Auxiliary Functions

        static readonly string[] suffixes =
       { "Байт", "КБ", "МБ", "ГБ", "ТБ", "ПБ" };
        public static string FormatSize(Int64 bytes)
        {
            int counter = 0;
            decimal number = (decimal)bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number = number / 1024;
                counter++;
            }
            return string.Format("{0:n1} {1}", number, suffixes[counter]);
        }

        public string getDirectoryName(string file_path)
        {
            return new DirectoryInfo(file_path).Name;
        }

        string getFileTypeTag(string extension)
        {
            switch (extension)
            {
                case ".xls":
                    return "fiv-cla fiv-icon-xls";


                case ".pdf":
                    return "fiv-cla fiv-icon-pdf";


                case ".wav":
                    return "fiv-cla fiv-icon-wav";


                case ".7z":
                    return "fiv-cla fiv-icon-7z";


                case ".aac":
                    return "fiv-cla fiv-icon-aac";


                case ".accdb":
                    return "fiv-cla fiv-icon-accdb";


                case ".accdt":
                    return "fiv-cla fiv-icon-accdt";


                case ".amr":
                    return "fiv-cla fiv-icon-amr";


                case ".apk":
                    return "fiv-cla fiv-icon-apk";


                case ".app":
                    return "fiv-cla fiv-icon-app";


                case ".asax":
                    return "fiv-cla fiv-icon-asax";


                case ".asc":
                    return "fiv-cla fiv-icon-asc";


                case ".ash":
                    return "fiv-cla fiv-icon-ash";


                case ".ashx":
                    return "fiv-cla fiv-icon-ashx";


                case ".asp":
                    return "fiv-cla fiv-icon-asp";


                case ".aspx":
                    return "fiv-cla fiv-icon-aspx";


                case ".avi":
                    return "fiv-cla fiv-icon-avi";


                case ".axd":
                    return "fiv-cla fiv-icon-axd";


                case ".bash":
                    return "fiv-cla fiv-icon-bash";


                case ".bat":
                    return "fiv-cla fiv-icon-bat";


                case ".bin":
                    return "fiv-cla fiv-icon-bin";


                case ".bmp":
                    return "fiv-cla fiv-icon-bmp";


                case ".bpg":
                    return "fiv-cla fiv-icon-bpg";


                case ".bz2":
                    return "fiv-cla fiv-icon-bz2";


                case ".c":
                    return "fiv-cla fiv-icon-c";


                case ".cab":
                    return "fiv-cla fiv-icon-cab";


                case ".cad":
                    return "fiv-cla fiv-icon-cad";


                case ".caf":
                    return "fiv-cla fiv-icon-caf";


                case ".cal":
                    return "fiv-cla fiv-icon-cal";


                case ".cd":
                    return "fiv-cla fiv-icon-cd";


                case ".cer":
                    return "fiv-cla fiv-icon-cer";

                case ".cfg":
                    return "fiv-cla fiv-icon-cfg";

                case ".cfm":
                    return "fiv-cla fiv-icon-cfm";

                case ".cfml":
                    return "fiv-cla fiv-icon-cfml";

                case ".cgi":
                    return "fiv-cla fiv-icon-cgi";

                case ".class":
                    return "fiv-cla fiv-icon-class";

                case ".cmd":
                    return "fiv-cla fiv-icon-cmd";

                case ".coffee":
                    return "fiv-cla fiv-icon-coffee";

                case ".com":
                    return "fiv-cla fiv-icon-com";

                case ".conf":
                    return "fiv-cla fiv-icon-conf";

                case ".cpp":
                    return "fiv-cla fiv-icon-cpp";

                case ".crdownload":
                    return "fiv-cla fiv-icon-crdownload";

                case ".cs":
                    return "fiv-cla fiv-icon-cs";

                case ".csh":
                    return "fiv-cla fiv-icon-csh";

                case ".cson":
                    return "fiv-cla fiv-icon-cson";

                case ".csproj":
                    return "fiv-cla fiv-icon-csproj";

                case ".css":
                    return "fiv-cla fiv-icon-css";

                case ".csv":
                    return "fiv-cla fiv-icon-csv";

                case ".cue":
                    return "fiv-cla fiv-icon-cue";

                case ".dat":
                    return "fiv-cla fiv-icon-dat";

                case ".db":
                    return "fiv-cla fiv-icon-db";

                case ".dbf":
                    return "fiv-cla fiv-icon-dbf";

                case ".deb":
                    return "fiv-cla fiv-icon-deb";

                case ".dgn":
                    return "fiv-cla fiv-icon-dgn";

                case ".dist":
                    return "fiv-cla fiv-icon-dist";

                case ".diz":
                    return "fiv-cla fiv-icon-diz";

                case ".dll":
                    return "fiv-cla fiv-icon-dll";

                case ".dmg":
                    return "fiv-cla fiv-icon-dmg";

                case ".dng":
                    return "fiv-cla fiv-icon-dng";

                case ".doc":
                    return "fiv-cla fiv-icon-doc";

                case ".docb":
                    return "fiv-cla fiv-icon-docb";

                case ".docm":
                    return "fiv-cla fiv-icon-docm";

                case ".docx":
                    return "fiv-cla fiv-icon-docx";

                case ".dot":
                    return "fiv-cla fiv-icon-dot";

                case ".dotm":
                    return "fiv-cla fiv-icon-dotm";

                case ".dotx":
                    return "fiv-cla fiv-icon-dotx";

                case ".download":
                    return "fiv-cla fiv-icon-download";

                case ".dpj":
                    return "fiv-cla fiv-icon-dpj";

                case ".dtd":
                    return "fiv-cla fiv-icon-dtd";

                case ".dwg":
                    return "fiv-cla fiv-icon-dwg";

                case ".dxf":
                    return "fiv-cla fiv-icon-dxf";

                case ".exe":
                    return "fiv-cla fiv-icon-exe";

                case ".f4v":
                    return "fiv-cla fiv-icon-f4v";

                case ".fax":
                    return "fiv-cla fiv-icon-fax";

                case ".fb2":
                    return "fiv-cla fiv-icon-fb2";

                case ".fla":
                    return "fiv-cla fiv-icon-fla";

                case ".flac":
                    return "fiv-cla fiv-icon-flac";

                case ".flv":
                    return "fiv-cla fiv-icon-flv";

                case ".gif":
                    return "fiv-cla fiv-icon-gif";

                case ".gpg":
                    return "fiv-cla fiv-icon-gpg";

                case ".gz":
                    return "fiv-cla fiv-icon-gz";

                case ".h":
                    return "fiv-cla fiv-icon-h";

                case ".heic":
                    return "fiv-cla fiv-icon-heic";

                case ".htm":
                    return "fiv-cla fiv-icon-htm";

                case ".html":
                    return "fiv-cla fiv-icon-html";

                case ".ico":
                    return "fiv-cla fiv-icon-ico";

                case ".ics":
                    return "fiv-cla fiv-icon-ics";

                case ".iff":
                    return "fiv-cla fiv-icon-iff";

                case ".ifo":
                    return "fiv-cla fiv-icon-ifo";

                case ".image":
                    return "fiv-cla fiv-icon-image";

                case ".inf":
                    return "fiv-cla fiv-icon-inf";

                case ".ini":
                    return "fiv-cla fiv-icon-ini";

                case ".jar":
                    return "fiv-cla fiv-icon-jar";

                case ".java":
                    return "fiv-cla fiv-icon-java";

                case ".jpe":
                    return "fiv-cla fiv-icon-jpe";

                case ".jpeg":
                    return "fiv-cla fiv-icon-jpeg";

                case ".jpg":
                    return "fiv-cla fiv-icon-jpg";

                case ".js":
                    return "fiv-cla fiv-icon-js";

                case ".json":
                    return "fiv-cla fiv-icon-json";

                case ".jsp":
                    return "fiv-cla fiv-icon-jsp";

                case ".jsx":
                    return "fiv-cla fiv-icon-jsx";

                case ".key":
                    return "fiv-cla fiv-icon-key";

                case ".lnk":
                    return "fiv-cla fiv-icon-lnk";

                case ".log":
                    return "fiv-cla fiv-icon-log";

                case ".lua":
                    return "fiv-cla fiv-icon-lua";

                case ".m":
                    return "fiv-cla fiv-icon-m";

                case ".m2v":
                    return "fiv-cla fiv-icon-m2v";

                case ".m3u":
                    return "fiv-cla fiv-icon-m3u";

                case ".m3u8":
                    return "fiv-cla fiv-icon-m3u8";

                case ".m4a":
                    return "fiv-cla fiv-icon-m4a";

                case ".m4v":
                    return "fiv-cla fiv-icon-m4v";

                case ".mdb":
                    return "fiv-cla fiv-icon-mdb";

                case ".mdf":
                    return "fiv-cla fiv-icon-mdf";

                case ".mid":
                    return "fiv-cla fiv-icon-mid";

                case ".midi":
                    return "fiv-cla fiv-icon-midi";

                case ".mkv":
                    return "fiv-cla fiv-icon-mkv";

                case ".mobi":
                    return "fiv-cla fiv-icon-mobi";

                case ".mod":
                    return "fiv-cla fiv-icon-mod";

                case ".mov":
                    return "fiv-cla fiv-icon-mov";

                case ".mp3":
                    return "fiv-cla fiv-icon-mp3";

                case ".mp4":
                    return "fiv-cla fiv-icon-mp4";

                case ".mpa":
                    return "fiv-cla fiv-icon-mpa";

                case ".mpd":
                    return "fiv-cla fiv-icon-mpd";

                case ".mpeg":
                    return "fiv-cla fiv-icon-mpeg";

                case ".mpg":
                    return "fiv-cla fiv-icon-mpg";

                case ".mpp":
                    return "fiv-cla fiv-icon-mpp";

                case ".mpt":
                    return "fiv-cla fiv-icon-mpt";

                case ".msi":


                case ".nef":
                    return "fiv-cla fiv-icon-nef";

                case ".nfo":
                    return "fiv-cla fiv-icon-nfo";

                case ".nix":
                    return "fiv-cla fiv-icon-nix";

                case ".odb":
                    return "fiv-cla fiv-icon-odb";

                case ".ods":
                    return "fiv-cla fiv-icon-ods";

                case ".odt":
                    return "fiv-cla fiv-icon-odt";

                case ".ogg":
                    return "fiv-cla fiv-icon-ogg";

                case ".ost":
                    return "fiv-cla fiv-icon-ost";


                case ".ott":
                    return "fiv-cla fiv-icon-ott";

                case ".pcd":
                    return "fiv-cla fiv-icon-pcd";

                case ".pdb":
                    return "fiv-cla fiv-icon-pdb";

                case ".pem":
                    return "fiv-cla fiv-icon-pem";

                case ".pfx":
                    return "fiv-cla fiv-icon-pfx";

                case ".pgp":
                    return "fiv-cla fiv-icon-pgp";

                case ".php":
                    return "fiv-cla fiv-icon-php";

                case ".png":
                    return "fiv-cla fiv-icon-png";

                case ".potx":
                    return "fiv-cla fiv-icon-potx";

                case ".pps":
                    return "fiv-cla fiv-icon-pps";

                case ".ppsx":
                    return "fiv-cla fiv-icon-ppsx";

                case ".ppt":
                    return "fiv-cla fiv-icon-ppt";

                case ".pptm":
                    return "fiv-cla fiv-icon-pptm";

                case ".pptx":
                    return "fiv-cla fiv-icon-pptx";

                case ".ps":
                    return "fiv-cla fiv-icon-ps";

                case ".psd":
                    return "fiv-cla fiv-icon-psd";

                case ".pst":
                    return "fiv-cla fiv-icon-pst";

                case ".pub":
                    return "fiv-cla fiv-icon-pub";

                case ".rar":
                    return "fiv-cla fiv-icon-rar";

                case ".resx":
                    return "fiv-cla fiv-icon-resx";

                case ".rom":
                    return "fiv-cla fiv-icon-rom";

                case ".rpm":
                    return "fiv-cla fiv-icon-rpm";

                case ".rsa":
                    return "fiv-cla fiv-icon-rsa";

                case ".rss":
                    return "fiv-cla fiv-icon-rss";

                case ".rtf":
                    return "fiv-cla fiv-icon-rtf";

                case ".sass":
                    return "fiv-cla fiv-icon-sass";

                case ".scss":
                    return "fiv-cla fiv-icon-scss";

                case ".sh":
                    return "fiv-cla fiv-icon-sh";

                case ".sldm":
                    return "fiv-cla fiv-icon-sldm";

                case ".sldx":
                    return "fiv-cla fiv-icon-sldx";

                case ".sln":
                    return "fiv-cla fiv-icon-sln";

                case ".sql":
                    return "fiv-cla fiv-icon-sql";

                case ".sqlite":
                    return "fiv-cla fiv-icon-sqlite";

                case ".step":
                    return "fiv-cla fiv-icon-step";

                case ".stl":
                    return "fiv-cla fiv-icon-stl";

                case ".svg":
                    return "fiv-cla fiv-icon-svg";

                case ".swd":
                    return "fiv-cla fiv-icon-swd";

                case ".swf":
                    return "fiv-cla fiv-icon-swf";

                case ".sys":
                    return "fiv-cla fiv-icon-sys";

                case ".tar":
                    return "fiv-cla fiv-icon-tar";

                case ".tcsh":
                    return "fiv-cla fiv-icon-tcsh";

                case ".tex":
                    return "fiv-cla fiv-icon-tex";

                case ".tga":
                    return "fiv-cla fiv-icon-tga";

                case ".tif":
                    return "fiv-cla fiv-icon-tif";

                case ".tiff":
                    return "fiv-cla fiv-icon-tiff";

                case ".tmp":
                    return "fiv-cla fiv-icon-tmp";

                case ".torrent":
                    return "fiv-cla fiv-icon-torrent";

                case ".tsv":
                    return "fiv-cla fiv-icon-tsv";

                case ".ttf":
                    return "fiv-cla fiv-icon-ttf";

                case ".txt":
                    return "fiv-cla fiv-icon-txt";

                case ".udf":
                    return "fiv-cla fiv-icon-udf";

                case ".vb":
                    return "fiv-cla fiv-icon-vb";

                case ".vbproj":
                    return "fiv-cla fiv-icon-vbproj";

                case ".vbs":
                    return "fiv-cla fiv-icon-vbs";

                case ".vcd":
                    return "fiv-cla fiv-icon-vcd";

                case ".vcs":
                    return "fiv-cla fiv-icon-vcs";

                case ".vdx":
                    return "fiv-cla fiv-icon-vdx";

                case ".vob":
                    return "fiv-cla fiv-icon-vob";

                case ".vsd":
                    return "fiv-cla fiv-icon-vsd";

                case ".vss":
                    return "fiv-cla fiv-icon-vss";

                case ".vst":
                    return "fiv-cla fiv-icon-vst";

                case ".vsx":
                    return "fiv-cla fiv-icon-vsx";

                case ".vtx":
                    return "fiv-cla fiv-icon-vtx";

                case ".war":
                    return "fiv-cla fiv-icon-war";

                case ".wbk":
                    return "fiv-cla fiv-icon-wbk";

                case ".webinfo":
                    return "fiv-cla fiv-icon-webinfo";

                case ".webm":
                    return "fiv-cla fiv-icon-webm";

                case ".webp":
                    return "fiv-cla fiv-icon-webp";

                case ".wma":
                    return "fiv-cla fiv-icon-wma";

                case ".wmf":
                    return "fiv-cla fiv-icon-wmf";

                case ".wmv":
                    return "fiv-cla fiv-icon-wmv";

                case ".xaml":
                    return "fiv-cla fiv-icon-xaml";

                case ".xlm":
                    return "fiv-cla fiv-icon-xlm";

                case ".xlsm":
                    return "fiv-cla fiv-icon-xlsm";

                case ".xlsx":
                    return "fiv-cla fiv-icon-xlsx";

                case ".xlt":
                    return "fiv-cla fiv-icon-xlt";

                case ".xltm":
                    return "fiv-cla fiv-icon-xltm";

                case ".xltx":
                    return "fiv-cla fiv-icon-xltx";

                case ".xml":
                    return "fiv-cla fiv-icon-xml";

                case ".xsd":
                    return "fiv-cla fiv-icon-xsd";

                case ".xz":
                    return "fiv-cla fiv-icon-xz";

                case ".yaml":
                    return "fiv-cla fiv-icon-yaml";

                case ".yml":
                    return "fiv-cla fiv-icon-yml";

                case ".zip":
                    return "fiv-cla fiv-icon-zip";


                default:
                    return "bx bx-save";
            }
        }

        private string SupressRussian(string s)
        {
            StringBuilder ret = new StringBuilder();

            string[] rus = { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й",
          "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц",
          "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я",
          "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й",
          "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц",
          "ч", "ш", "ш", "ъ", "ы", "ь", "э", "ю", "я" ,
          " ", "#","$","@","+","=","*","@","!","\"","^","&"};

            string[] eng = { "A", "B", "V", "G", "D", "E", "E", "ZH", "Z", "I", "Y",
          "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "KH", "TS",
          "CH", "SH", "SHCH", null, "Y", null, "E", "YU", "YA",
          "a", "b", "v", "g", "d", "e", "e", "zh", "z", "i", "y",
          "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "kh", "ts",
          "ch", "sh", "shch", null, "y", null, "e", "yu", "ya",
          "_","_","_","_","_","_","_","_","_","_","_","_"};

            bool step_completed = false;

            for (int j = 0; j < s.Length; j++)
            {
                for (int i = 0; i < rus.Length; i++)
                {
                    if (s.Substring(j, 1) == rus[i])
                    {
                        ret.Append(eng[i]);
                        step_completed = true;
                    }
                }
                if (!step_completed)
                {
                    ret.Append(s[j]);
                }
                step_completed = false;

            }


            return ret.ToString();
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            if (filename.Contains("/"))
                filename = filename.Substring(filename.LastIndexOf("/") + 1);

            return filename;
        }

        private string GetPathAndFilename(string filename, string iwebhost)
        {
            string filename_lcl = filename;
            if (System.IO.File.Exists(Path.Combine(iwebhost, "docs", filename_lcl)))
            {
                string filename_without_ext = Path.GetFileNameWithoutExtension(filename_lcl);
                filename_without_ext += Guid.NewGuid().GetHashCode();
                filename_without_ext += Path.GetExtension(filename_lcl);

                filename_lcl = filename_without_ext;
            }

            return filename_lcl;
        }

        #endregion

        #region FileUploading
        string file_fileName { get; set; }
        long file_UploadedPercentage { get; set; }
        long file_Size { get; set; }
        long file_UploadedBytes { get; set; }

        private List<IBrowserFile> loadedFiles = new();
        private long maxFileSize = 1024 * 15 * 1024;
        private int maxAllowedFiles = 100;
        private bool isLoading;

        List<string> attach_lst = new List<string>();
        List<string> attach_lst_links = new List<string>();

        string FormatBytes(long value)
      => ByteSize.FromBytes(value).ToString("fi2", CultureInfo.CurrentCulture);

        private async Task LoadFilesAttachments(InputFileChangeEventArgs e)
        {
            string filename = "";
            isLoading = true;
            loadedFiles.Clear();

            int file_num = -1;

            foreach (var file in e.GetMultipleFiles(maxAllowedFiles))
            {
                file_num++;
                try
                {
                    loadedFiles.Add(file);
                    filename = file.Name;
                    var trustedFileNameForFileStorage = Path.GetRandomFileName();
                    var path = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads", "calendar", filename);

                    if (System.IO.File.Exists(Path.Combine(_env.ContentRootPath, "wwwroot", "uploads", "calendar", filename)) || filename.Length > 100)
                    {
                        string filename_we = Path.GetFileNameWithoutExtension(file.Name);
                        string extension = Path.GetExtension(file.Name);
                        filename = getUnicalFileName(filename_we, extension);
                    }

                    sc.Log("CalendarTasks", "Начинается загрузка файла '" + filename + "'", SystemCore.LogLevels.Info);

                    if (file.Size > maxFileSize)
                    {
                        //attach_lst_status.Add("");
                        sc.Log("CalendarTasks", "Файл слишком большой, поэтому инициализирую загрузку большого файла '" + filename + "'", SystemCore.LogLevels.Debug);
                        await OnInputFileChange(e, filename, file_num);
                    }
                    else
                    {
                        await using FileStream fs = new(path, FileMode.Create);
                        await file.OpenReadStream(maxFileSize).CopyToAsync(fs);
                    }
                }
                catch (Exception ex)
                {
                    sc.Log("CalendarTasks", "Ошибка при загрузке файла '" + filename + "', " + ex.Message, SystemCore.LogLevels.Error);
                }
                finally
                {
                    attach_lst.Add(filename);
                    attach_lst_links.Add("https://evicrm.store/uploads/calendar/" + filename);
                    sc.Log("CalendarTasks", "Загружен файл '" + filename + "'", SystemCore.LogLevels.Info);
                    StateHasChanged();
                }
            }
        }

        string getUnicalFileName(string filename_we, string extension)
        {
            string guid_str = Guid.NewGuid().ToString();
            return (filename_we + "_" + Path.GetRandomFileName() + extension);
        }

        bool _uploading = false;

        async Task OnInputFileChange(InputFileChangeEventArgs e, string unique_filename, int FileNumber)
        {

            await using var timer = new Timer(_ => InvokeAsync(() => StateHasChanged()));
            timer.Change(TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500));

            const long CHUNKSIZE = 1024 * 400; // subjective

            IBrowserFile file = null;

            if (e.FileCount > 1)
            {
                int file_c = 0;
                var file_dump_col = e.GetMultipleFiles(e.FileCount);
                foreach (IBrowserFile file_in in file_dump_col)
                {
                    if (file_c == FileNumber)
                    {
                        file = file_dump_col[file_c];
                    }
                    file_c++;
                }
            }
            else
            {
                file = e.File;
            }

            long uploadedBytes = 0;
            long totalBytes = file.Size;
            long percent = 0;
            int fragment = 0;
            long chunkSize;

            var clientlocal = ClientFactory.CreateClient("LocalApi");

            using (var inStream = file.OpenReadStream(long.MaxValue))
            {
                _uploading = true;
                while (_uploading)
                {
                    try
                    {
                        chunkSize = CHUNKSIZE;
                        if (uploadedBytes + CHUNKSIZE > totalBytes)
                        {
                            chunkSize = totalBytes - uploadedBytes;
                        }
                        var chunk = new byte[chunkSize];
                        await inStream.ReadAsync(chunk, 0, chunk.Length);
                        using var formFile = new MultipartFormDataContent();
                        var fileContent = new StreamContent(new MemoryStream(chunk));
                        formFile.Add(fileContent, "file", unique_filename);
                        var response = await clientlocal.PostAsync($"api/FileStoreUpload/{fragment++}", formFile);
                        uploadedBytes += chunkSize;
                        percent = uploadedBytes * 100 / totalBytes;

                        file_UploadedPercentage = percent;
                        file_Size = totalBytes;
                        file_UploadedBytes = uploadedBytes;
                        file_fileName = file.Name;

                        sc.Log("CalendarTasks", "Начинается загрузка большого файла '" + unique_filename + "'", SystemCore.LogLevels.Info);
                        sc.Log("CalendarTasks", $"Загружено {percent}%  {uploadedBytes} из {totalBytes} | Фрагмент: {fragment}", SystemCore.LogLevels.Debug);

                        if (percent >= 100)
                        {
                            //Загрузка завершена

                            _uploading = false;
                            file_UploadedPercentage = 0;
                            file_Size = 0;
                            file_UploadedBytes = 0;
                            file_fileName = "";
                        }
                        await InvokeAsync(StateHasChanged);
                    }
                    catch (Exception ex)
                    {
                        sc.Log("CalendarTasks", "Произошла ошибка при загрузке большого файла '" + unique_filename + "'", SystemCore.LogLevels.Error);
                    }
                }
            }
        }

        record FileUploadProgress(string FileName, long Size)
        {
            public long UploadedBytes { get; set; }
            public double UploadedPercentage => (double)UploadedBytes / (double)Size * 100d;
        }
        #endregion

        public string FileSystemManagerUp_Alexandra(string root = "")
        {
            string new_dir = Path.GetDirectoryName(root);

            // if (new_dir == null || new_dir == Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot"))
            if (false)
            {
                return root;
            }
            return new_dir;
        }

        public string FileSystemManagerUp_Exchange(string root = "")
        {
            string new_dir = Path.GetDirectoryName(root);
            if (new_dir == null || new_dir == Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot",user_))
            {
                return root;
            }
            return new_dir;
        }

        public void FileSystemManagerKiller_Exchange(string root = "")
        {
            using (var conn = new FtpClient(sftp_url_server, sftp_user, sftp_password, sftp_url_port))
            {
                conn.Connect();
                conn.DeleteFile(root);
            }
        }

        public string FileSystemManagerDownload_Exchange(string root = "")
        {
            string url_file = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "temp", root);

            using (var conn = new FtpClient(sftp_url_server, sftp_user, sftp_password, sftp_url_port))
            {
                conn.Connect();
                conn.DownloadFile(url_file, root);
            }

            return url_file;
        }

        public string Alexandra_FileDownloader(string url = "")
        {
            if (!url.Contains(System.IO.Directory.GetCurrentDirectory()))
            {
                return "about:blank";
            }
            else
            {
                string clear_path = url;
                clear_path = clear_path.Replace(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "Файлы Александры"), "");
                clear_path = "Файлы Александры" + clear_path;
                clear_path = clear_path.Replace("\\", "/");
                return clear_path;
            }

            return "about:blank";
        }


        [HttpGet("/api/fsm_alexandra")]
        public List<FileSystemEntityModel> FileSystemManager_Alexandra(string root = "")
        {
            List<FileSystemEntityModel> fileSystemEntityModels = new List<FileSystemEntityModel>();

            string file_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Файлы Александры");

            if (root != "")
            {
                file_path = root;
            }

            string[] files_arr = Directory.GetFiles(file_path);


            string[] dirs_arr = Directory.GetDirectories(file_path);
            List<string> dirs = new List<string>();
            foreach (string dir in dirs_arr)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(file_path, dir));
                FileSystemEntityModel fsem = new FileSystemEntityModel();

                fsem.name = dir;
                fsem.isFile = false;
                fsem.date_modifed = directoryInfo.LastWriteTime;
                fsem.file_size = 0;
                fsem.icon_tag = "mdi mdi-folder-zip";

                fileSystemEntityModels.Add(fsem);
                dirs.Add(dir);
            }

            List<string> files = new List<string>();
            foreach (string file in files_arr)
            {
                FileInfo fileInfo = new FileInfo(Path.Combine(file_path, file));
                FileSystemEntityModel fsem = new FileSystemEntityModel();

                fsem.name = file;
                fsem.isFile = true;
                fsem.date_modifed = fileInfo.LastWriteTime;
                fsem.file_size = fileInfo.Length;
                fsem.icon_tag = getFileTypeTag(Path.GetExtension(file));

                fileSystemEntityModels.Add(fsem);
                files.Add(file);
            }

            return fileSystemEntityModels;
        }

        [HttpGet("/api/fsm_exchange")]
        public List<FileSystemEntityModel> FileSystemManager_Exchange(string root = "")
        {
            List<FileSystemEntityModel> entities = new List<FileSystemEntityModel>();

            using (var conn = new FtpClient(sftp_url_server, sftp_user, sftp_password, sftp_url_port))
            {
                conn.Connect();
                conn.SetWorkingDirectory(root);

                var elems = conn.GetListing();

                if (elems != null)
                    foreach (var elem in elems)
                    {
                        FileSystemEntityModel fsem = new FileSystemEntityModel();

                        fsem.name = Path.Combine(root, elem.Name);
                        fsem.date_modifed = elem.Modified;
                        fsem.file_size = elem.Size;

                        if (elem.Type == FtpObjectType.Directory)
                        {
                            fsem.isFile = false;
                            fsem.icon_tag = "mdi mdi-folder-zip";
                        }
                        else
                        {
                            fsem.icon_tag = getFileTypeTag(Path.GetExtension(elem.Name));
                            fsem.isFile = true;
                        }
                        entities.Add(fsem);
                    }
            }

            return entities;
        }

        public async Task FileSystemSelectFile_Exchange(string path = "", string iwebhost = "")
        {
            using (var conn = new FtpClient(sftp_url_server, sftp_user, sftp_password, sftp_url_port))
            {
                conn.Connect();
                conn.DownloadFile(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "temp", path), path);
            }
        }

    }
}
