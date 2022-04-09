using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CommonMaxTools.Tools
{
    public class DataSerializator
    {
        #region Constants

        private const string DefaultExtension = ".data";

        #endregion Constants

        #region Fields

        private string _folderPath;
        private string _extension;
        private IFormatter _formatter;

        #endregion Fields

        #region Constructors

        public DataSerializator(string folderPath) : this(folderPath, DefaultExtension, new BinaryFormatter()) { }

        public DataSerializator(string folderPath, string extension, IFormatter formatter)
        {
            _folderPath = folderPath;
            _extension = extension;
            _formatter = formatter;

            Directory.CreateDirectory(folderPath);
        }

        #endregion Constructors

        #region Public Methods

        public void SaveOnDisk<T>(string fileName, T data, bool append = false)
        {
            ValidateParameters(fileName, typeof(T));
            string filePath = GetFilePath(fileName);
            FileMode mode = append ? FileMode.Append : FileMode.Create;

            using (FileStream stream = new FileStream(filePath, mode, FileAccess.Write))
            {
                _formatter.Serialize(stream, data);
            }
        }

        public T LoadFromDisk<T>(string fileName)
        {
            ValidateParameters(fileName, typeof(T));
            string filePath = GetFilePath(fileName);

            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return (T)_formatter.Deserialize(stream);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void ValidateParameters(string fileName, Type type)
        {
            if (!IsTypeSerializable(type))
                throw new ArgumentException($"{type} is not supported serialization.");

            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentException("String is null or empty", nameof(fileName));
        }

        private bool IsTypeSerializable(Type type)
        {
            return type.GetCustomAttributes(typeof(SerializableAttribute), true).FirstOrDefault() != null;
        }

        private string GetFilePath(string fileName)
        {
            return Path.Combine(_folderPath, $"{fileName}{_extension}");
        }

        #endregion Private Methods
    }
}
