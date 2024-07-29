import React, { useEffect, useState } from 'react'
import { cakeModel } from '../../../Interface';
import CakeCard from './CakeCard';
import { useGetCakesQuery } from '../../../API/cakeApi';
import { useDispatch } from 'react-redux';
import { setCake } from '../../../Storage/Redux/cakeSlice';
import { MainLoader } from '../Shared';

function CakeList() {
    //const [cakes, setCakes] = useState<cakeModel[]>([]);

    const {data, isLoading} = useGetCakesQuery(null);
    const dispatch = useDispatch();

    useEffect(() => {
      if(!isLoading){
        dispatch(setCake(data.result))
      }
    }, [isLoading]);

    if(isLoading){
      return <MainLoader/>
    }


    return (
        <div className="container row">
        {data.result.length > 0 && data.result.map((cakes: cakeModel, index:number) => (
          <CakeCard cake = {cakes} key={index} />
        ))}
        </div>
    )
}

export default CakeList