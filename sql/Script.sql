

--use [C:\caminho_diretorio_projeto\FI.WEBATIVIDADEENTREVISTA\APP_DATA\BANCODEDADOS.MDF]
--go


BEGIN

	SET NOCOUNT ON;
	
	BEGIN TRY
		BEGIN TRANSACTION	
			

			BEGIN
				ALTER TABLE CLIENTES ADD CPF [VARCHAR](11) NOT NULL
			END
			
			
			SELECT 0 AS Code, 'Sucesso!' AS Msg;

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT ERROR_NUMBER() AS Code, ERROR_MESSAGE() AS Msg;
	END CATCH

END