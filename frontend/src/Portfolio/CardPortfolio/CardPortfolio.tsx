import React, { SyntheticEvent } from "react";
import DeletePortfolio from "../DeletePortfolio/DeletePortfolio";
import { Link } from "react-router-dom";
interface Props {
    portfolioValue: string;
    onPortfolioDelete: (e: SyntheticEvent) => void
};
// <p></p> is used to define the paragrah
const CardPortfolio = ({ portfolioValue, onPortfolioDelete }: Props) => {
  return (
    <div className="flex flex-col w-full p-8 space-y-4 text-center rounded">
      <Link to ={`/company/${portfolioValue}`} className="pt-6 text-xl font-bold">{portfolioValue}</Link>
      <DeletePortfolio
        portfolioValue={portfolioValue}
        onPortfolioDelete={onPortfolioDelete}
      />
    </div>
  );
};

export default CardPortfolio;