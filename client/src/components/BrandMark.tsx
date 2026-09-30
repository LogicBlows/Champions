type BrandMarkProps = {
  size?: 'default' | 'large';
};

export default function BrandMark({ size = 'default' }: BrandMarkProps) {
  return (
    <div className={`brand-mark${size === 'large' ? ' brand-mark--large' : ''}`}>
      <span className="brand-mark-icon" aria-hidden="true">
        <svg viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path
            d="M13 2L4 14h7l-1 8 9-12h-7l1-8z"
            fill="currentColor"
          />
        </svg>
      </span>
      <span className="brand-mark-text">Champion Portal</span>
    </div>
  );
}
