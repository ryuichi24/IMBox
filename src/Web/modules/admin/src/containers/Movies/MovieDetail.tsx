import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { MovieModel, MemberModel } from '@IMBoxWeb/core/dist/models';
import { memberService, movieService } from '@IMBoxWeb/core/dist/services';
import { InputField } from '@/components/InputField';
import { PrimaryBtn, SecondaryBtn } from '@/components/UI';

export const MovieDetail = () => {
  const { movieId } = useParams();
  const navigate = useNavigate();
  const [memberList, setMemberList] = useState<MemberModel[]>([]);

  const [movieTitle, setMovieTitle] = useState<string>('');
  const [movieDescription, setMovieDescription] = useState<string | ''>('');
  const [movieMainPosterUrl, setMovieMainPosterUrl] = useState<string | ''>('');
  const [movieMainTrailerUrl, setMovieMainTrailerUrl] = useState<string | ''>('');
  const [movieMembers, setMovieMembers] = useState<string[]>(['']);

  const setInputs = (fetchedMovie: MovieModel) => {
    setMovieTitle(fetchedMovie.title || '');
    setMovieDescription(fetchedMovie.description || '');
    setMovieMainPosterUrl(fetchedMovie.mainPosterUrl || '');
    setMovieMainTrailerUrl(fetchedMovie.mainTrailerUrl || '');
    setMovieMembers(fetchedMovie.members?.map((memberItem) => memberItem.id) as any);
  };

  useEffect(() => {
    if (!movieId) return;

    (async () => {
      try {
        const fetchedMovie = await movieService.getById({ movieId });
        setInputs(fetchedMovie);
      } catch (error) {
        console.error(error);
        alert((error as any)?.response?.data || (error as any).message);
      }
    })();
  }, [movieId]);

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

  const handleFormSubmit: React.FormEventHandler<HTMLFormElement> = async (e) => {
    try {
      if (!movieId) return;
      e.preventDefault();

      const movieToUpdate: MovieModel = {
        title: movieTitle === '' ? undefined : movieTitle,
        description: movieDescription === '' ? undefined : movieDescription,
        mainPosterUrl: movieMainPosterUrl === '' ? undefined : movieMainPosterUrl,
        mainTrailerUrl: movieMainTrailerUrl === '' ? undefined : movieMainTrailerUrl,
        memberIds: movieMembers,
      };

      await movieService.update({ movie: movieToUpdate, movieId });

      alert('successfully updated');
    } catch (error) {
      console.error(error);
      alert((error as any)?.response?.data || (error as any).message);
    }
  };

  const handleDelete = async () => {
    if (!movieId) return;
    const confirmed = confirm('Are you sure you want to delete it?');
    if (!confirmed) return;
    try {
      await movieService.remove({ movieId });
      alert('Successfully removed');
      navigate('/admin/movies');
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="p-4 h-100" style={{ overflow: 'scroll' }}>
      <div className="card h-100 p-4">
        <div className="d-flex justify-content-end">
          <button className="btn btn-danger" style={{ width: '80px' }} onClick={handleDelete}>
            Delete
          </button>
        </div>
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
              required={true}
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
            <SecondaryBtn btnText="Close" onClick={() => navigate('/admin/movies')} />
            <PrimaryBtn btnText="Save" type="submit" />
          </div>
        </form>
      </div>
    </div>
  );
};
