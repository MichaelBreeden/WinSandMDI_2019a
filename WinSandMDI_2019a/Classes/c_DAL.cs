using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WinSandMDI_2019a.Classes
{
    public class c_DAL
    {
        public static void runSQLQuery(string strConnectionString, string strQuery, DataSet ds, out string strError, string strLocation = "")
        {
            strError = String.Empty;
            //ds.Reset();
            //for (int iCounter = ds.Tables.Count; iCounter > 0; iCounter--)
            //    ds.Tables.RemoveAt(iCounter - 1);
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter())
                        {
                            connection.ConnectionString = strConnectionString;
                            command.CommandType = CommandType.Text;
                            command.CommandText = strQuery;

                            adapter.SelectCommand = command;
                            connection.Open();
                            command.Connection = connection;
                            adapter.Fill(ds);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                strError = "ErrorSQL(241)-" + strLocation + "-" + sqlEx.Message;
            }
            catch (Exception ex)
            {
                strError = "SQL Error(245)" + strLocation + "-" + ex.Message;
            }
            return;
        }

        public void runExecuteNonQuery(string strConnectionString, string strQuery, out string strError, string strLocation = "")
        {
            strError = String.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        connection.ConnectionString = strConnectionString;
                        command.CommandType = CommandType.Text;
                        command.CommandText = strQuery;

                        connection.Open();
                        command.Connection = connection;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                strError = "ErrorSQL(267)-" + strLocation + "-" + sqlEx.Message;
            }
            catch (Exception ex)
            {
                strError = "Error(271)-" + strLocation + "-" + ex.Message;
            }
            return;
        }

        public void runExecuteScalar(string strConnectionString, string strQuery, out int iCount, out int iSQLErrorNumber, out string strError, out string strExMsg)
        {
            iCount = 0;
            iSQLErrorNumber = 0;
            strError = String.Empty;
            strExMsg = String.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        connection.ConnectionString = strConnectionString;
                        command.CommandType = CommandType.Text;
                        command.CommandText = strQuery;

                        connection.Open();
                        command.Connection = connection;
                        iCount = (Int32)command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                iSQLErrorNumber = sqlEx.Number;
                strExMsg = sqlEx.Message;
                strError = "ErrorSQL";
            }
            catch (Exception ex)
            {
                strExMsg = ex.Message;
                strError = "Error";
            }
            return;
        }

#if SQL_ADO_TEMPLATE_CODE

USE [CorrFax]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CorrFax_Lookup](
	[CorrFax_LookupID] [int] IDENTITY(1000,1) NOT NULL,
	[Category] [varchar](64) NOT NULL,
	[SubCategory] [varchar](64) NOT NULL,
	[SystemID] [int] NOT NULL,
	[sValue] [varchar](512) NOT NULL,
	[iValue] [int] NOT NULL,
	[Description] [varchar](256) NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CorrFax_Lookup] ADD  DEFAULT ('') FOR [SubCategory]
GO

ALTER TABLE [dbo].[CorrFax_Lookup] ADD  DEFAULT ('') FOR [sValue]
GO

ALTER TABLE [dbo].[CorrFax_Lookup] ADD  DEFAULT ((0)) FOR [iValue]
GO

ALTER TABLE [dbo].[CorrFax_Lookup] ADD  DEFAULT ('') FOR [Description]
GO

USE [CorrFax]
GO

/****** Object:  StoredProcedure [dbo].[sp_CorrFax_Lookup_Select]    Script Date: 11/20/2019 7:48:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[sp_CorrFax_Lookup_Select]
/*---------------------------------------------------------------------------------
--  Creation Date: 
-- 
--  Copyright: 
-- 
--  Written by: Tim Pruitt
-- 
--  Purpose: Gets record from table CorrFax_Lookup
-- 
--  Result Set: none  
--  
--  Tables (readonly): CorrFax_Lookup
--    
--  Tables (updated) : none  
--    
--  Return Status: 0=success, any other=error
--    
--  Calls: (none)  
--   
*/---------------------------------------------------------------------------------
     @RetCode int out
	,@RetStr varchar(255) out
	,@Category varchar(64)
	,@SubCategory varchar(64)
    ,@SystemID  int
AS
BEGIN
	set @RetCode = 0
	SET NOCOUNT ON

Begin Try
if @Category = '' and  @SubCategory = '' and @SystemID = 0
  begin
	select [CorrFax_LookupID],[Category],[SubCategory],[SystemID],[sValue],[iValue],[Description]
	from [CorrFax_Lookup]
  end
else if @Category = '' and  @SubCategory = '' 
  begin
	select [CorrFax_LookupID],[Category],[SubCategory],[SystemID],[sValue],[iValue],[Description]
	from [CorrFax_Lookup]
		where [SystemID] = @SystemID
  end
else if @SystemID = 0
  begin
	select [CorrFax_LookupID],[Category],[SubCategory],[SystemID],[sValue],[iValue],[Description]
	from [CorrFax_Lookup]
		where [Category]=@Category and [SubCategory]=@SubCategory
  end
else
  begin
	select [CorrFax_LookupID],[Category],[SubCategory],[SystemID],[sValue],[iValue],[Description]
	from [CorrFax_Lookup]
		where [Category]=@Category and [SubCategory]=@SubCategory and [SystemID] = @SystemID
  end
  set @RetCode = @@ROWCOUNT

End Try
Begin Catch
	Set @RetCode = Error_Number()
	Set @RetStr = Error_Message()
End Catch
	Return @RetCode
END
GO

**********************************************************
/// <summary>
/// This one is just for updating a single row in the MailingRec_Lookup table
/// </summary>
/// <param name="cErrorToken">To return any errors</param>
/// <param name="cLookupToken">To send and receive lookup values</param>
public void do_sp_UpdateMailingReq_Lookup(c_ErrorToken cErrorToken, c_LookupToken cLookupToken)
{
    cErrorToken.Reset();
    strCurrentMethod = "do_sp_UpdateMailingReq_Lookup()-" + cLookupToken.ToString() + ".";
    using (SqlConnection connection = new SqlConnection())
    {
        connection.ConnectionString = this.strConnectionString;
        using (SqlCommand command = new SqlCommand())
        {
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_UpdateMailingReq_Lookup";

            command.Parameters.Add("@Category", SqlDbType.VarChar, 64);
            command.Parameters.Add("@SubCategory", SqlDbType.VarChar, 64);
            command.Parameters.Add("@SystemID", SqlDbType.Int);
            command.Parameters.Add("@sValue", SqlDbType.VarChar, 512);
            command.Parameters.Add("@iValue", SqlDbType.Int);

            command.Parameters.Add("@RetCode", SqlDbType.Int);
            command.Parameters["@RetCode"].Direction = ParameterDirection.Output;
            command.Parameters.Add("@RetStr", SqlDbType.VarChar, 255);
            command.Parameters["@RetStr"].Direction = ParameterDirection.Output;

            command.Parameters["@Category"].Value = cLookupToken.Category;
            command.Parameters["@SubCategory"].Value = cLookupToken.SubCategory;
            command.Parameters["@SystemID"].Value = cLookupToken.SystemID;
            command.Parameters["@sValue"].Value = cLookupToken.sValue;
            command.Parameters["@iValue"].Value = cLookupToken.iValue;
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                if (String.IsNullOrEmpty(command.Parameters["@RetStr"].Value.ToString()) == false)
                    cErrorToken.setAllErrorsStrings("SQL Error(D1137):" + command.Parameters["@RetStr"].Value.ToString() + ". Code=" + command.Parameters["@RetCode"].Value.ToString() + ".", "", strCurrentMethod);
            }
            catch (SqlException sqlEx)
            {
                cErrorToken.setAllErrorsStrings("SQL Error(D1391)", sqlEx.Message.ToString(), strCurrentMethod);
            }
            catch (Exception ex)
            {
                cErrorToken.setAllErrorsStrings("Error(D1195)", ex.Message.ToString(), strCurrentMethod);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

/// <summary>
/// This method is just for getting rows from the MailingRec_Lookup table
/// The Stored Procedure will make three responces
/// 1. if @Category = '' and  @SubCategory = '' ... where [SystemID] = @SystemID
/// 2. else if @SystemID = 0 ... where [Category]=@Category and [SubCategory]=@SubCategory
/// 3. else ... where [Category]=@Category and [SubCategory]=@SubCategory and [SystemID] = @SystemID
/// #Note that this is duplicated in DalMon.cs
/// </summary>
/// <param name="cErrorToken">To return any errors</param>
/// <param name="cLookupToken">To send and receive lookup values</param>
public void do_sp_GetMailingReq_Lookup(c_ErrorToken cErrorToken, List<c_LookupToken> lstcLookupToken,
                                 string strCategory, string strSubCategory, int iRenderingSystemID)   
{
    DataSet dataset = new DataSet();
    cErrorToken.Reset();
    strCurrentMethod = "do_sp_GetMailingReq_Lookup()-strCategory=" + strCategory + ", strSubCategory=" 
        + strSubCategory + ", iRenderingSystemID=" + iRenderingSystemID.ToString() + ".";
    try
    {
        using (SqlConnection connection = new SqlConnection())
        {
            using (SqlCommand command = new SqlCommand())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    connection.ConnectionString = this.strConnectionString;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_GetMailingReq_Lookup";

                    command.Parameters.Add("@Category", SqlDbType.VarChar, 64);
                    command.Parameters.Add("@SubCategory", SqlDbType.VarChar, 64);
                    command.Parameters.Add("@SystemID", SqlDbType.Int);

                    command.Parameters.Add("@RetCode", SqlDbType.Int);
                    command.Parameters["@RetCode"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@RetStr", SqlDbType.VarChar, 255);
                    command.Parameters["@RetStr"].Direction = ParameterDirection.Output;

                    command.Parameters["@Category"].Value = strCategory;
                    command.Parameters["@SubCategory"].Value = strSubCategory;
                    command.Parameters["@SystemID"].Value = iRenderingSystemID;

                    adapter.SelectCommand = command;
                    connection.Open();
                    command.Connection = connection;
                    adapter.Fill(dataset);
                    if (String.IsNullOrEmpty(command.Parameters["@RetStr"].Value.ToString()) == false)
                        cErrorToken.setAllErrorsStrings("SQL Error(D1568):" + command.Parameters["@RetStr"].Value.ToString() + ". Code=" + command.Parameters["@RetCode"].Value.ToString() + ".", "", strCurrentMethod);

                    lstcLookupToken.Clear(); // get rid of any already there
                    if (dataset.Tables.Count > 0)
                    {
                        //if (dataset.Tables[0].Rows.Count > 0)
                        //{
                        foreach (DataRow dr in dataset.Tables[0].Rows)
                        {
                            c_LookupToken cLookupToken = new c_LookupToken();
                            cLookupToken.Category = dr["Category"].ToString();
                            cLookupToken.SubCategory = dr["SubCategory"].ToString();
                            cLookupToken.sValue = dr["sValue"].ToString();
                            string str = dr["iValue"].ToString();
                            int ii = 0;
                            if (Int32.TryParse(str, out ii) == true)
                                cLookupToken.iValue = ii;
                            str = dr["SystemID"].ToString();
                            ii = 0;
                            if (Int32.TryParse(str, out ii) == true)
                                cLookupToken.SystemID = ii;
                            lstcLookupToken.Add(cLookupToken);
                        }
                        //}
                    }
                }
            }
        }
    }
    catch (SqlException sqlEx)
    {
        cErrorToken.setAllErrorsStrings("SQL Error(D1599)", sqlEx.Message.ToString(), strCurrentMethod);
    }
    catch (Exception ex)
    {
        cErrorToken.setAllErrorsStrings("Error(D1603)", ex.Message.ToString(), strCurrentMethod);
    }
    return;
}

#endif

    } // End public class c_DAL
}
