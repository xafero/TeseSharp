using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Text;
using Kajabity.Tools.Java;

namespace TeseSharp
{
	public class Tese
	{
		private const string emptyPrefix = "";
		
		public T Deserialize<T>(string text)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			using (MemoryStream mem = new MemoryStream(bytes)) {
				return Deserialize<T>(mem);
			}
		}
		
		public T Deserialize<T>(Stream stream)
		{
			try {
				JavaProperties props = new JavaProperties();
				props.Load(stream);
				return (T)DeserializeFields(emptyPrefix, props, typeof(T));
			} catch (IOException e) {
				throw new TeseReadException(e);
			}
		}
		
		private object DeserializeFields(string prefix, IDictionary props, Type type)
		{
			try {
				object obj = Activator.CreateInstance(type);
				FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
				foreach (FieldInfo field in fields)
					DeserializeField(prefix, obj, field, props);
				return obj;
			} catch (Exception e) {
				throw new TeseReadException(e);
			}
		}
		
		private void DeserializeField(string prefix, object obj, FieldInfo field, IDictionary props)
		{
			try {
				string key = field.Name;
				string objKey = string.Format("{0}.{1}", prefix, key);
				object val = props[objKey];
				if (val == null) {
					if (FindLongerKey(props, objKey)) {
						// field.setAccessible(true);
						field.SetValue(obj, DeserializeFields(objKey, props, field.FieldType));
					}
					return;
				}
				// field.setAccessible(true);
				field.SetValue(obj, FromStr(val.ToString(), field));
			} catch (Exception e) {
				throw new TeseReadException(e);
			}
		}
		
		private bool FindLongerKey(IDictionary props, string shortKey)
		{
			foreach (object key in props.Keys)
				if (key.ToString().StartsWith(shortKey, StringComparison.InvariantCulture))
					return true;
			return false;
		}
		
		private object FromStr(string val, FieldInfo field)
		{
			Type type = field.FieldType;
			if (type.IsEnum)
				return Enum.Parse(type, val);
			CultureInfo cult = CultureInfo.InvariantCulture;
			switch (type.Name) {
				case "Boolean":
					return Boolean.Parse(val);
				case "Byte":
					return Byte.Parse(val);
				case "Char":
					return Char.Parse(val);
				case "Single":
					return Single.Parse(val, cult);
				case "Double":
					return Double.Parse(val, cult);
				case "Int32":
					return Int32.Parse(val);
				case "Int16":
					return Int16.Parse(val);
				case "Int64":
					return Int64.Parse(val);
				case "BigInteger":
					return BigInteger.Parse(val, cult);
				case "Decimal":
					return decimal.Parse(val, cult);
				case "DateTime":
					return DateTime.Parse(val, cult).ToUniversalTime();
				case "String":
					return val;
				default:
					throw new InvalidOperationException(type + " is not supported!");
			}
		}

		public string Serialize(object obj)
		{
			byte[] bytes;
			using (MemoryStream mem = new MemoryStream()) {
				Serialize(obj, mem);
				bytes = mem.ToArray();
			}
			return Encoding.UTF8.GetString(bytes);
		}
		
		public void Serialize(object obj, Stream writer)
		{
			try {
				JavaProperties props = new JavaProperties();
				SerializeFields(emptyPrefix, obj, props);
				props.Store(writer, null);
			} catch (IOException e) {
				throw new TeseWriteException(e);
			}
		}
		
		private void SerializeFields(string prefix, object obj, IDictionary props)
		{
			Type type = obj.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
			foreach (FieldInfo field in fields)
				SerializeField(prefix, obj, field, props);
		}
		
		private void SerializeField(string prefix, object obj, FieldInfo field, IDictionary props)
		{
			try {
				string key = field.Name;
				// field.setAccessible(true);
				object val = field.GetValue(obj);
				string objKey = string.Format("{0}.{1}", prefix, key);
				try {
					props[objKey] = ToStr(val, field);
				} catch (InvalidOperationException) {
					SerializeFields(objKey, val, props);
				}
			} catch (Exception e) {
				throw new TeseWriteException(field, e);
			}
		}
		
		private string ToStr(object value, FieldInfo field)
		{
			Type type = field.FieldType;
			if (type.IsEnum)
				return value.ToString();
			CultureInfo cult = CultureInfo.InvariantCulture;
			switch (type.Name) {
				case "DateTime":
					return ((DateTime)value).ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz");
				case "Single":
					return ((float)value).ToString(cult);
				case "Double":
					return ((double)value).ToString(cult);
				case "Decimal":
					return ((decimal)value).ToString(cult);
				case "Boolean":
				case "BigInteger":					
				case "Int64":
				case "Char":
				case "Int32":
				case "Byte":
				case "Int16":					
				case "String":
					return value.ToString();
				default:
					throw new InvalidOperationException(type + " is not supported!");
			}
		}
	}
}