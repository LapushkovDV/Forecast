SELECT *
      
  FROM [TnkDB].[dbo].[Forecasts]

  Where --CONVERT(date, Forecast_recommendation_date)	  = CONVERT(date, GETDATE())
  --Forecast_recommendation = 'Покупать'  and  
  Ticker = 'Meli '
  --and CONVERT(date,Forecast_recommendation_date) = DATEFROMPARTS(2021,12,27)
  order by Forecast_recommendation_date