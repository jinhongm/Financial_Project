import React, { SyntheticEvent } from "react";
import "./Card.css"; // 确保这是正确的CSS路径
import { CompanySearch } from "../../company"; // 确保这是正确的导入路径
import AddPortfolio from "../../Portfolio/AddPortfolio/AddPortfolio"; // 确保这是正确的导入路径
import { Link } from "react-router-dom";

interface Props {
  id: string;
  searchResults: CompanySearch;
  onPortfolioCreate: (e: SyntheticEvent) => void;
}

const Card: React.FC<Props> = ({ id, searchResults, onPortfolioCreate }) => {
  return (
    <div className="flex flex-col items-center justify-between w-full p-6 bg-slate-100 rounded-lg md:flex-row" id={id} key={id}>
      <div>
        <Link to={`company/${searchResults.symbol}`}className="font-bold text-center text-veryDarkViolet md:text-left">
          {searchResults.name} ({searchResults.symbol})
        </Link>
        <p className="text-veryDarkBlue">
          {searchResults.currency}
        </p>
        <p className="font-bold text-veryDarkBlue">
          {searchResults.exchangeShortName} - {searchResults.stockExchange}
        </p>
      </div>
      <AddPortfolio onPortfolioCreate={onPortfolioCreate} symbol={searchResults.symbol} />
    </div>
  );
};

export default Card;
