#pragma once

#include <exception>
#include "ErrorCode.h"

namespace AngouriMath
{
    class AngouriMathException : public std::exception
    {
    public:
        AngouriMathException(ErrorCode e)
            : error(e) { }

        virtual const char* what() const override
        {
            return this->Name().c_str();
        }

        const std::string& Name() const
        {
            return this->error.Name();
        }

        const std::string& StackTrace() const
        {
            return this->error.StackTrace();
        }

    private:
        ErrorCode error;
    };
}