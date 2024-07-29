import React, { useState } from 'react'
import { Link } from 'react-router-dom';
import { cakeModel } from '../../../Interface';
import { useUpdateShoppingCartMutation } from '../../../API/shoppingCartApi';
import { Loader } from '../Shared';

interface Props {
    cake: cakeModel;
}

function CakeCard(props: Props) {
  const [isAddingToCart, setIsAddingToCart] = useState<boolean>(false);
  const[updateShoppingCart] = useUpdateShoppingCartMutation(); 

  const handleAddToCart = async (cakeId:number) =>{
    setIsAddingToCart(true);

    const response = await updateShoppingCart({
      cakeId:cakeId, 
      quantity:1, 
      userId:'1b41deca-f2da-4a1e-815d-74d498181ca0'
    });

    setIsAddingToCart(false);
  }


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

          {isAddingToCart?(
            <div style={{
              position:"absolute",
              top:"15px",
              right:"15px"
            }}>
              <Loader/>
          </div>
          ) :(

          <i className="bi bi-cart-plus btn btn-outline-danger add-to-cart"
          onClick={() => handleAddToCart(props.cake.cakeId)}
          ></i>
        )}
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
