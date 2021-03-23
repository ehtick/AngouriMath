﻿/*
 * Copyright (c) 2019-2021 Angouri.
 * AngouriMath is licensed under MIT.
 * Details: https://github.com/asc-community/AngouriMath/blob/master/LICENSE.md.
 * Website: https://am.angouri.org.
 */

#include "AngouriMath.CPP.h"
#include "Imports.h"

namespace AngouriMath
{
    struct HandleDeleter
    {
        void operator()(const Internal::EntityRef* handle)
        {
            if (handle != nullptr)
            {
                auto error = free_entity(*handle);
                delete handle;
            }
        }
    };

    Entity::Entity(Internal::EntityRef handle)
        : handle(new Internal::EntityRef(handle), HandleDeleter())
    {
        *this->handle = handle;
    }

    Internal::EntityRef ParseString(const char* expr)
    {
        Internal::EntityRef result;
        HandleErrorCode(maths_from_string(expr, &result));
        return result;
    }

    Internal::EntityRef ParseString(const char* expr, ErrorCode& e)
    {
        Internal::EntityRef result;
        HandleErrorCode(maths_from_string(expr, &result), e);
        return result;
    }

    Entity::Entity()
        : handle(nullptr, HandleDeleter())
    {
    }

    Entity::Entity(const std::string& expr)
        : Entity(expr.c_str())
    {
    }

    Entity::Entity(const char* expr)
        : Entity(ParseString(expr))
    {
    }

    Entity::Entity(const std::string& expr, ErrorCode& e)
        : Entity(expr.c_str(), e)
    {
    }

    Entity::Entity(const char* expr, ErrorCode& e)
        : Entity(ParseString(expr, e))
    {
    }

    std::string Entity::ToString() const
    {
        char* buff = nullptr;
        HandleErrorCode(entity_to_string(*this->handle, &buff));
        return buff != nullptr ? std::string(buff) : std::string();
    }

    std::string Entity::ToString(ErrorCode& ec) const
    {
        char* buff = nullptr;
        HandleErrorCode(entity_to_string(*this->handle, &buff), ec);
        return buff != nullptr ? std::string(buff) : std::string();
    }

    Entity Entity::Differentiate(const Entity& var) const
    {
        Internal::EntityRef result;
        HandleErrorCode(entity_differentiate(*this->handle, *var.handle, &result));
        return Entity(result);
    }

    Entity Entity::Differentiate(const Entity& var, ErrorCode& ec) const
    {
        Internal::EntityRef result;
        HandleErrorCode(entity_differentiate(*this->handle, *var.handle, &result), ec);
        return Entity(result);
    }

    Internal::EntityRef GetHandle(const Entity& e)
    {
        return *e.handle;
    }

    Entity CreateByHandle(Internal::EntityRef handle)
    {
        return Entity(handle);
    }
}