//using System;
//using System.Text.Json;
//using System.Text.Json.Serialization;

//namespace OFF.LogViewer
//{

//    internal sealed class JsonMatrixConverter<T> : JsonConverter<T[,]>
//    {
//        #region Methods

//        public override T[,] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//        {
//            var json = reader.GetString();

//            var jaggedArray = JsonSerializer.Deserialize<T[][]>(json, options);

//            var nRows = jaggedArray.Length;
//            var nColumns = jaggedArray[0].Length;

//            var matrix = new T[nRows, nColumns];

//            for (var i = 0; i < nRows; i++)
//            for (var j = 0; j < nColumns; j++)
//                matrix[i, j] = jaggedArray[i][j];

//            return matrix;
//        }

//        public override void Write(Utf8JsonWriter writer, T[,] value, JsonSerializerOptions options)
//        {
//            var nRows = value.GetLength(0);
//            var nColumns = value.GetLength(1);

//            var jaggedArray = new T[nRows][];

//            for (var i = 0; i < nRows; i++)
//            {
//                var array = new T[nColumns];

//                for (var j = 0; j < nColumns; j++)
//                    array[j] = value[i, j];

//                jaggedArray[i] = array;
//            }

//            var json = JsonSerializer.SerializeToUtf8Bytes(jaggedArray, options);

//            writer.WriteStringValue(json);
//        }

//        #endregion
//    }

//}