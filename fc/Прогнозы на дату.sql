SELECT f.Ticker as '����������'
	   ,f.Forecast_show_name as '������������ ��������'
	   ,f.Forecast_analyst as '��������'
	   ,f.Forecast_company as '�������� ��������� �������'
	   ,f.Forecast_recommendation as '������������'
	   ,f.Forecast_recommendation_date as '���� ��������'
	   ,f.Forecast_current_price as '������� ��������� (�)'
	   ,f.Forecast_target_price as '������� ��������� (�)'
	   ,CAST(f.Forecast_target_price as DECIMAL(9,2))-CAST(f.Forecast_current_price as DECIMAL(9,2)) as '������� �-�'
	   ,f.Forecast_price_change_rel as '������� � ���������'
FROM Forecasts f
WHERE cast(f.Forecast_recommendation_date as date)  >= cast('24-12-2022' as date)
and cast(f.Forecast_recommendation_date as date)  <= cast('28-12-2022' as date)

