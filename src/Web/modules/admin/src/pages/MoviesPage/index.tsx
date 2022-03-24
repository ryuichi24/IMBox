import React, { useEffect, useState } from 'react';
import { MovieModel } from '@/models/movie-model';
import { movieService } from '@/services/movie-service';
import { ReusableModal } from '@/components/ReusableModal';
import { PrimaryBtn, SecondaryBtn } from '@/components/UI';
import { InputField } from '@/components/InputField';
import { MemberModel } from '@/models/member-model';
import { memberService } from '@/services/member-service';
import { MovieItem } from '@/components/MovieItem';

export const MoviesPaage = () => {
  const [movieList, setMovieList] = useState<MovieModel[]>([]);
  const [memberList, setMemberList] = useState<MemberModel[]>([]);

  useEffect(() => {
    (async () => {
      try {
        const movies = await movieService.get({ page: 1 });
        setMovieList(movies);
      } catch (error) {
        console.error(error);
        alert((error as any)?.response?.data || (error as any).message);
      }
    })();
  }, []);

  useEffect(() => {
    (async () => {
      try {
        const members = await memberService.get({ page: 1 });
        setMemberList(members);
      } catch (error) {
        console.error(error);
        alert((error as any)?.response?.data || (error as any).message);
      }
    })();
  }, []);

  const [showFormModal, setShowFormModal] = useState(false);
  const closeFormModal = () => {
    setShowFormModal(false);
  };

  const [movieTitle, setMovieTitle] = useState<string | undefined>(undefined);
  const [movieDescription, setMovieDescription] = useState<string | undefined>(undefined);
  const [movieMainPosterUrl, setMovieMainPosterUrl] = useState<string | undefined>(undefined);
  const [movieMainTrailerUrl, setMovieMainTrailerUrl] = useState<string | undefined>(undefined);
  const [movieMembers, setMovieMembers] = useState<string[]>([]);

  const clearInputs = () => {
    setMovieTitle(undefined);
    setMovieDescription(undefined);
    setMovieMainPosterUrl(undefined);
    setMovieMainTrailerUrl(undefined);
    setMovieMembers([]);
  };

  const handleFormSubmit: React.FormEventHandler<HTMLFormElement> = async (e) => {
    try {
      e.preventDefault();

      const newMovie: MovieModel = {
        title: movieTitle,
        description: movieDescription,
        mainPosterUrl: movieMainPosterUrl,
        mainTrailerUrl: movieMainTrailerUrl,
        memberIds: movieMembers,
      };

      await movieService.create({ movie: newMovie });

      alert('successfully created');
      setMovieList((prev) => [...prev, newMovie]);
      clearInputs();
    } catch (error) {
      console.error(error);
      alert((error as any)?.response?.data || (error as any).message);
    }
  };

  return (
    <>
      <div className="p-4 h-100" style={{ overflow: 'scroll' }}>
        <div className="mb-3 d-flex w-100 justify-content-between">
          <h2>Movies</h2>
          <button className="btn btn-primary" onClick={(e) => setShowFormModal(true)}>
            + New movie
          </button>
        </div>
        <div className="card h-100 d-flex flex-row" style={{ gap: '1rem' }}>
          {movieList.map((movieItem) => (
            <MovieItem key={movieItem.id} movieItem={movieItem} />
          ))}
        </div>
      </div>

      <ReusableModal show={showFormModal}>
        <>New Movie</>
        <>
          <form onSubmit={handleFormSubmit}>
            <div className="mb-3">
              <InputField
                label="Title"
                id="movieTitle"
                type="text"
                onChange={(e) => setMovieTitle(e.target.value)}
                value={movieTitle}
              />
            </div>
            <div className="mb-3">
              <InputField
                multiLine
                maxRows={3}
                label="Description"
                id="movieDescription"
                type="text"
                onChange={(e) => setMovieDescription(e.target.value)}
                value={movieDescription}
              />
            </div>
            <div className="mb-3">
              <InputField
                label="Main poster"
                id="movieMainPoster"
                type="url"
                placeholder="https//example.com/image.jpeg"
                formText="Please paste URL of the image"
                onChange={(e) => setMovieMainPosterUrl(e.target.value)}
                value={movieMainPosterUrl}
              />
            </div>
            <div className="mb-3">
              <InputField
                label="Main trailer"
                id="movieMainTrailer"
                type="url"
                placeholder="https//example.com/image.mp4"
                formText="Please paste URL of the video"
                onChange={(e) => setMovieMainTrailerUrl(e.target.value)}
                value={movieMainTrailerUrl}
              />
            </div>
            <div className="mb-3">
              <label htmlFor="movieMembers" className="form-label">
                Members
              </label>
              <select
                id="movieMembers"
                className="form-select"
                multiple
                value={movieMembers}
                size={5}
                onChange={(e) => setMovieMembers(Array.from(e.target.selectedOptions, (option) => option.value))}
              >
                {memberList.map((memberItem) => (
                  <option key={memberItem.id} value={memberItem.id}>{`${memberItem.name} (${memberItem.role})`}</option>
                ))}
              </select>
              <div className="form-text">
                For windows: Hold down the control (ctrl) button to select multiple options.
              </div>
              <div className="form-text">For Mac: Hold down the command button to select multiple options</div>
            </div>

            <div className="d-flex justify-content-end" style={{ gap: '1rem' }}>
              <SecondaryBtn btnText="Close" onClick={closeFormModal} type="submit" />
              <PrimaryBtn btnText="Save" />
            </div>
          </form>
        </>
      </ReusableModal>
    </>
  );
};
