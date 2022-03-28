import React from 'react';
import { Demographic } from '@IMBoxWeb/core/dist/models';
import { RatingAnalyticsItem } from '@/containers/RatingDetail/RatingAnalyticsItem';

export const RatingsPage = () => {
  const demographicTypes = [
    {
      name: 'All',
      query: Demographic.all,
    },
    {
      name: 'Males',
      query: Demographic.males,
    },
    {
      name: 'Females',
      query: Demographic.females,
    },
    {
      name: 'Aged under 18',
      query: Demographic.agedUnder18,
    },
    {
      name: 'Aged 18 to 29',
      query: Demographic.aged18To29,
    },
    {
      name: 'Aged 30 to 44',
      query: Demographic.aged30To44,
    },
    {
      name: 'Aged 45+',
      query: Demographic.aged45Plus,
    },

    {
      name: 'Males aged under 18',
      query: Demographic.malesAgedUnder18,
    },
    {
      name: 'Males aged 18 to 29',
      query: Demographic.malesAged18To29,
    },
    {
      name: 'Males aged 30 to 44',
      query: Demographic.malesAged30To44,
    },
    {
      name: 'Males aged 45+',
      query: Demographic.malesAged45Plus,
    },

    {
      name: 'Females aged under 18',
      query: Demographic.femalesAgedUnder18,
    },
    {
      name: 'Females aged 18 to 29',
      query: Demographic.femalesAged18To29,
    },
    {
      name: 'Females aged 30 to 44',
      query: Demographic.femalesAged30To44,
    },
    {
      name: 'Females aged 45+',
      query: Demographic.femalesAged45Plus,
    },
  ];
  return (
    <>
      <h2>Ratings page</h2>
      <div className="d-flex flex-wrap" style={{ gap: '1rem' }}>
        {demographicTypes.map((demographicTypeItem, index) => (
          <RatingAnalyticsItem key={index} demographicType={demographicTypeItem} />
        ))}
      </div>
    </>
  );
};
