2019開發
提供連結資料庫的工具，資料庫種類含有: SQL Server、Oracle、PostgreSQL
回傳型別: List<string>、DataTable



# SQL Server
```asp.net
ConSSDB cs = new ConSSDB();
cs.conDbStr = '';

string sql = @"SELECT *
				 FROM User
				WHERE ID = @IdName
				  AND GroupId like '%' + @GroupId + '%'";
cs.InitConnAndSetSQL(sql);

SqlParmeter[] ParmeterArr = { new SqlParameter("@IdName", SqlDbType.VarChar, 50),
							  new SqlParameter("@GroupId", SqlDbType.VarChar, 50)};
ParmeterArr[0].Value = "Yves";
ParmeterArr[1].Value = ";1;";
//設定參數數值及型別
cs.SetOrReSetParameter(ParmeterArr);//傳入並設定參數
List<string> result = new List<string>(cs.DoSqlAndGetResultToList());//執行SQL並取得回傳值

ParmeterArr[0].Value = "David";
ParmeterArr[1].Value = ";2;";
cs.SetOrReSetParameter(ParmeterArr);//傳入並設定參數
List<string> result2 = new List<string>(cs.DoSqlAndGetResultToList());//執行SQL並取得回傳值

cs.CloseConn();
```

# PostgreSQL
```asp.net
ConNPGDB cs = new ConNPGDB();
cs.conDbStr = '';

string sql = @"SELECT *
				 FROM User
				WHERE ID = @IdName
				  AND GroupId like '%' + @GroupId + '%'";
cs.InitConnAndSetSQL(sql);

//using Npgsql(需從NuGet安裝)
NpgsqlParameter[] ParmeterArr = { new NpgsqlParameter("@IdName", NpgsqlTypes.NpgsqlDbType.Varchar, 50),
								  new NpgsqlParameter("@GroupId", NpgsqlTypes.NpgsqlDbType.Varchar, 50)};
ParmeterArr[0].Value = "Yves";
ParmeterArr[1].Value = ";1;";
//設定參數數值及型別
cs.SetOrReSetParameter(ParmeterArr);//傳入並設定參數
List<string> result = new List<string>(cs.DoSqlAndGetResultToList());//執行SQL並取得回傳值

cs.CloseConn();
```

# Oracle
```asp.net
ConODB cs = new ConODB();
cs.conDbStr = '';

string sql = @"SELECT *
				 FROM User
				WHERE ID = :IdName
				  AND GroupId like '%' + :GroupId + '%'";
cs.InitConnAndSetSQL(sql);

//using Oracle.ManagedDataAccess.Client(需從NuGet安裝)
OracleParameter[] ParmeterArr = { new OracleParameter("@IdName", OracleDbType.Varchar2, 50),
								  new OracleParameter("@GroupId", OracleDbType.Varchar2, 50)};
ParmeterArr[0].Value = "Yves";
ParmeterArr[1].Value = ";1;";
//設定參數數值及型別
cs.SetOrReSetParameter(ParmeterArr);//傳入並設定參數
List<string> result = new List<string>(cs.DoSqlAndGetResultToList());//執行SQL並取得回傳值

cs.CloseConn();
```