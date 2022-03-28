import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend } from 'chart.js';
import ChartDataLabels from 'chartjs-plugin-datalabels';
import { Bar } from 'react-chartjs-2';
import { Demographic, RatingAnalyticsDTO } from '@IMBoxWeb/core/dist/models';
import { ratingService } from '@IMBoxWeb/core/dist/services';

interface Props {
  demographicType: { name: string; query: Demographic };
}

export const RatingAnalyticsItem = ({ demographicType }: Props) => {
  const { movieId } = useParams();
  const [analyticsItem, setAnalyticsItem] = useState<RatingAnalyticsDTO>();

  const labels = ['Rate 1', 'Rate 2', 'Rate 3', 'Rate 4', 'Rate 5'];

  useEffect(() => {
    if (!movieId) return;

    (async () => {
      const fetchedAnalyticsItem = await ratingService.getRatingAnalytics({
        demographic: demographicType.query,
        movieId,
      });
      setAnalyticsItem(fetchedAnalyticsItem);
    })();
  }, []);

  useEffect(() => {
    ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend, ChartDataLabels);
  }, []);

  return (
    <div className="card bg-dark p-2" style={{ width: '300px', height: '400px' }}>
      <h4>{demographicType.name}</h4>
      <div style={{ color: '#929292', marginRight: '0.5rem' }}>
        Average rate: <span className="fw-bold text-white">{analyticsItem?.averageRating?.toFixed(1)}</span>
      </div>
      <div style={{ color: '#929292', marginRight: '0.5rem' }}>
        Total vote : <span className="fw-bold text-white">{analyticsItem?.totalRatingVoteCount}</span> votes
      </div>
      <div>
        {analyticsItem && (
          <Bar
            options={{
              indexAxis: 'y' as const,
              elements: {
                bar: {
                  borderWidth: 3,
                },
              },
              responsive: true,
              plugins: {
                legend: {
                  display: false,
                },
                title: {
                  display: true,
                  text: demographicType.name,
                },
                datalabels: {
                  display: true,
                  color: 'white',
                  formatter: (value, context) => `${value}%`,
                  anchor: 'end',
                  offset: -35,
                  align: 'start',
                },
              },
              layout: {
                padding: {
                  left: 0,
                  right: 30,
                  top: 0,
                  bottom: 0,
                },
              },
              scales: {
                xAxis: {
                  min: 0,
                  max: 100,
                },
                yAxis: {
                  ticks: {
                    autoSkip: false,
                    stepSize: 1,
                  },
                },
              },
            }}
            data={{
              labels,
              datasets: [
                {
                  data: [...analyticsItem?.ratings?.map((ratingItem) => ratingItem.percent)],
                  borderColor: 'rgb(66, 66, 66)',
                  backgroundColor: 'rgb(251, 255, 0)',
                },
              ],
            }}
          />
        )}
      </div>
      <div>
        <ul className="p-3" style={{ listStyleType: 'none' }}>
          {analyticsItem?.ratings?.map((ratingItem, index) => (
            <li className="" key={index}>
              <span style={{ color: '#929292', marginRight: '0.5rem' }}>{`Rate ${ratingItem.rating}:`}</span>
              <span className="fw-bold">{`${ratingItem.ratingVoteCount} `}</span>
              <span style={{ fontSize: '12px' }}>votes</span>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};
