import React from 'react';

import { useAppDispatch, useAppSelector } from '../../redux/hooks'
import { addAmount, increment } from '../../redux/Features/counter-slice';

const ReduxTest: React.FunctionComponent = () => {

    const counter = useAppSelector((state) => state.counter.value);
    const dispatch = useAppDispatch();

    const handleClickIncrement = () => {
        dispatch(increment());
    }
    const handleClickAddAmount = () => {
        dispatch(addAmount(5));
    }

    return (
        <div className='Home'>
            <button onClick={handleClickIncrement}>
                Simple: {counter}
            </button>

            <button onClick={handleClickAddAmount}>
                Hard: {counter}
            </button>
        </div>
    );
};

export default ReduxTest;