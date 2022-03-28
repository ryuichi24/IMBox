import React, { useMemo, useState } from 'react';

interface Props {
  rating: number;
  onRating?: (rate: number) => void;
  count?: number;
  color?: {
    filled: string;
    unfilled: string;
  };
  displayOnly?: boolean;
}

export const StarRating = ({
  count = 5,
  rating = 0,
  color = { filled: '#fff424', unfilled: '#DCDCDC' },
  onRating,
  displayOnly = false,
}: Props) => {
  const [hoverRating, setHoverRating] = useState(0);

  const getColor = (index: number) => {
    if (hoverRating >= index) {
      return color.filled;
    } else if (!hoverRating && rating >= index) {
      return color.filled;
    }

    return color.unfilled;
  };

  const starRating = useMemo(() => {
    return Array(count)
      .fill(0)
      .map((_, i) => i + 1)
      .map((idx) => (
        <i
          key={idx}
          className="fa-solid fa-star cursor-pointer"
          onClick={() => {
            if (displayOnly || !onRating) return;
            onRating(idx);
          }}
          style={{ color: getColor(idx) }}
          onMouseEnter={() => {
            if (displayOnly) return;
            setHoverRating(idx);
          }}
          onMouseLeave={() => {
            if (displayOnly) return;
            setHoverRating(0);
          }}
          role="none"
        ></i>
      ));
  }, [count, rating, hoverRating]);

  return <div>{starRating}</div>;
};
