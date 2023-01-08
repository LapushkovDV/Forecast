SELECT f.Ticker as 'Инструмент'
	   ,f.Forecast_show_name as 'Наименование компании'
	   ,f.Forecast_analyst as 'Аналитик'
	   ,f.Forecast_company as 'Компания сделавшая прогноз'
	   ,f.Forecast_recommendation as 'Рекомендация'
	   ,f.Forecast_recommendation_date as 'Дата прогноза'
	   ,f.Forecast_current_price as 'Текущая стоимость (Т)'
	   ,f.Forecast_target_price as 'Целевая стоимость (Ц)'
	   ,CAST(f.Forecast_target_price as DECIMAL(9,2))-CAST(f.Forecast_current_price as DECIMAL(9,2)) as 'Разница Ц-Т'
	   ,f.Forecast_price_change_rel as 'Разница в процентах'
FROM Forecasts f
WHERE cast(f.Forecast_recommendation_date as date)  >= cast('24-12-2022' as date)
and cast(f.Forecast_recommendation_date as date)  <= cast('28-12-2022' as date)

