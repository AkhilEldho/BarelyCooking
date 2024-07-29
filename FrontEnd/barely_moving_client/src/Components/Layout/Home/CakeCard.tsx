import React from 'react'
import { Link } from 'react-router-dom';
import { cakeModel } from '../../../Interface';

interface Props {
    cake: cakeModel;
}

function CakeCard(props: Props) {
  return (
    <div className="col-md-4 col-12 p-4 d-flex justify-content-center">
      <div className="card custom-box-shadow">
        <div className="card-body pt-2">
          <div className="row col-10 p-4">
            <Link to={`/cake/${props.cake.cakeId}`}>
              <img
                src={props.cake.imageFile}
                alt=""
                className="fixed-size-image mt-5"
              />
            </Link>
          </div>

          {props.cake.specialTags && props.cake.specialTags.length > 0 && (
            <i className="bi bi-star btn btn-success special-tags">
              &nbsp; {props.cake.specialTags}
            </i>
          )}

          <i className="bi bi-cart-plus btn btn-outline-danger add-to-cart"></i>

          <div className="text-center">
            <p className="card-title m-0 text-success fs-3">
              <Link to={`/cake/${props.cake.cakeId}`} className="cake-link">
                {props.cake.name}
              </Link>
            </p>
            <p className="badge bg-secondary small-text">
              {props.cake.category}
            </p>
          </div>
          <div className="row text-center">
            <h4>${props.cake.price}</h4>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CakeCard;
